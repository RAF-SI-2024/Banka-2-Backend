using System.Collections.Concurrent;

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Database.Core;
using Bank.Link.Core;
using Bank.Permissions.Services;
using Bank.UserService.BackgroundServices;
using Bank.UserService.Database;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Mappers;
using Bank.UserService.Models;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ITransactionService
{
    Task<Result<Page<TransactionResponse>>> GetAll(TransactionFilterQuery transactionFilterQuery, Pageable pageable);

    Task<Result<Page<TransactionResponse>>> GetAllByAccountId(Guid accountId, TransactionFilterQuery transactionFilterQuery, Pageable pageable);

    Task<Result<TransactionResponse>> GetOne(Guid id);

    Task<Result<Transaction>> Create(TransactionCreateRequest transactionCreateRequest);

    Task<Result<TransactionResponse>> Update(TransactionUpdateRequest transactionUpdateRequest, Guid id);

    Task<Result<Transaction>> PrepareInternalTransaction(PrepareInternalTransaction prepareTransaction);

    Task ProcessInternalTransaction(ProcessTransaction processTransaction);
}

public class TransactionService(
    ITransactionRepository                      transactionRepository,
    IAccountRepository                          accountRepository,
    ICurrencyRepository                         currencyRepository,
    IDatabaseContextFactory<ApplicationContext> contextFactory,
    IExchangeService                            exchangeService,
    Lazy<TransactionBackgroundService>          transactionBackgroundServiceLazy,
    Lazy<IDataService>                          dataServiceLazy,
    IAuthorizationServiceFactory                authorizationServiceFactory,
    ITransactionCodeRepository                  transactionCodeRepository,
    IBankUserData                               bankUserData,
    IUserRepository                             userRepository
) : ITransactionService
{
    private readonly ITransactionRepository                      m_TransactionRepository            = transactionRepository;
    private readonly ITransactionCodeRepository                  m_TransactionCodeRepository        = transactionCodeRepository;
    private readonly IAccountRepository                          m_AccountRepository                = accountRepository;
    private readonly ICurrencyRepository                         m_CurrencyRepository               = currencyRepository;
    private readonly IAuthorizationServiceFactory                m_AuthorizationServiceFactory      = authorizationServiceFactory;
    private readonly IExchangeService                            m_ExchangeService                  = exchangeService;
    private readonly IUserRepository                             m_UserRepository                   = userRepository;
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory                   = contextFactory;
    private readonly IBankUserData                               m_BankUserData                     = bankUserData;
    private readonly Lazy<TransactionBackgroundService>          m_TransactionBackgroundServiceLazy = transactionBackgroundServiceLazy;
    private readonly Lazy<IDataService>                          m_DataServiceLazy                  = dataServiceLazy;

    private TransactionBackgroundService TransactionBackgroundService => m_TransactionBackgroundServiceLazy.Value;
    private IDataService                 Data                         => m_DataServiceLazy.Value;

    public async Task<Result<Page<TransactionResponse>>> GetAll(TransactionFilterQuery transactionFilterQuery, Pageable pageable)
    {
        var page = await m_TransactionRepository.FindAll(transactionFilterQuery, pageable);

        var transactionResponses = page.Items.Select(transaction => transaction.ToResponse())
                                       .ToList();

        return Result.Ok(new Page<TransactionResponse>(transactionResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<TransactionResponse>> GetOne(Guid id)
    {
        var transaction = await m_TransactionRepository.FindById(id);

        if (transaction is null)
            return Result.NotFound<TransactionResponse>($"No Transaction found with Id: {id}");

        var authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        if (authorizationService.Permissions == Permission.Client && transaction.FromAccount?.ClientId != authorizationService.UserId &&
            transaction.ToAccount?.ClientId  != authorizationService.UserId)
            return Result.Unauthorized<TransactionResponse>();

        return Result.Ok(transaction.ToResponse());
    }

    public async Task<Result<Page<TransactionResponse>>> GetAllByAccountId(Guid accountId, TransactionFilterQuery transactionFilterQuery, Pageable pageable)
    {
        var authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        var page = await m_TransactionRepository.FindAllByAccountId(accountId, transactionFilterQuery, pageable);

        if (authorizationService.Permissions == Permission.Client &&
            page.Items.Any(transaction => transaction.FromAccount!.ClientId != authorizationService.UserId && transaction.ToAccount!.ClientId != authorizationService.UserId))
            return Result.Forbidden<Page<TransactionResponse>>();

        var transactionResponses = page.Items.Select(transaction => transaction.ToResponse())
                                       .ToList();

        return Result.Ok(new Page<TransactionResponse>(transactionResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<Transaction>> Create(TransactionCreateRequest transactionCreateRequest)
    {
        var transaction = await CreateTransaction(transactionCreateRequest);

        if (transaction.Value == null)
            return Result.BadRequest<Transaction>("Not going thru.");

        return Result.Ok(transaction.Value);
    }

    public async Task<Result<TransactionResponse>> Update(TransactionUpdateRequest request, Guid id)
    {
        var dbTransaction = await m_TransactionRepository.FindById(id);

        if (dbTransaction is null)
            return Result.NotFound<TransactionResponse>($"No Transaction found with Id: {id}");

        var transaction = await m_TransactionRepository.Update(dbTransaction.Update(request));

        return Result.Ok(transaction.ToResponse());
    }

    public async Task<Result<Transaction>> CreateTransaction(TransactionCreateRequest createTransaction)
    {
        if (createTransaction.FromAccountNumber == null && createTransaction.ToAccountNumber == null)
            return Result.BadRequest<Transaction>("No valid account provided.");

        var fromAccountTask     = m_AccountRepository.FindByAccountNumber(createTransaction.FromAccountNumber);
        var fromCurrencyTask    = m_CurrencyRepository.FindById(createTransaction.FromCurrencyId);
        var toAccountTask       = m_AccountRepository.FindByAccountNumber(createTransaction.ToAccountNumber);
        var toCurrencyTask      = m_CurrencyRepository.FindById(createTransaction.ToCurrencyId);
        var transactionCodeTask = m_TransactionCodeRepository.FindById(createTransaction.CodeId);
        var exchangeDetailsTask = m_ExchangeService.CalculateExchangeDetails(createTransaction.FromCurrencyId, createTransaction.ToCurrencyId);

        await Task.WhenAll(fromAccountTask, fromCurrencyTask, toAccountTask, toCurrencyTask, transactionCodeTask, exchangeDetailsTask);

        var fromAccount     = await fromAccountTask;
        var fromCurrency    = await fromCurrencyTask;
        var toAccount       = await toAccountTask;
        var toCurrency      = await toCurrencyTask;
        var transactionCode = await transactionCodeTask;
        var exchangeDetails = await exchangeDetailsTask;

        var bankCodeFrom = createTransaction.FromAccountNumber?[..3];
        var bankCodeTo   = createTransaction.ToAccountNumber?[..3];

        if (transactionCode is null)
            return Result.BadRequest<Transaction>("Invalid transaction code.");

        if (transactionCode.Code == Seeder.TransactionCode.TransactionCode266.Code) // Withdraw
            return await PrepareFromAccountTransaction(createTransaction.ToPrepareFromAccountTransaction(transactionCode, fromAccount, fromCurrency),
                                                       TransactionBackgroundService.InternalTransactions);

        if (transactionCode.Code == Seeder.TransactionCode.TransactionCode276.Code || transactionCode.Code == Seeder.TransactionCode.TransactionCode277.Code) // Installment
            return await PrepareDirectFromAccountTransaction(createTransaction.ToPrepareDirectFromAccountTransaction(transactionCode, fromAccount, fromCurrency));

        if (transactionCode.Code == Seeder.TransactionCode.TransactionCode265.Code) // Deposit
            return await PrepareToAccountTransaction(createTransaction.ToPrepareToAccountTransaction(transactionCode, toAccount, toCurrency),
                                                     TransactionBackgroundService.InternalTransactions);

        if (transactionCode.Code == Seeder.TransactionCode.TransactionCode263.Code || transactionCode.Code == Seeder.TransactionCode.TransactionCode270.Code ||
            transactionCode.Code == Seeder.TransactionCode.TransactionCode271.Code) // Loan, Agent
            return await PrepareDirectToAccountTransaction(createTransaction.ToPrepareDirectToAccountTransaction(transactionCode, toAccount, toCurrency));

        var isSecurity = transactionCode.Code == Seeder.TransactionCode.TransactionCode280.Code || transactionCode.Code == Seeder.TransactionCode.TransactionCode286.Code;

        if (isSecurity && createTransaction.FromAccountNumber is null) // Security
            return await PrepareToAccountTransaction(createTransaction.ToPrepareToAccountTransaction(transactionCode, toAccount, toCurrency),
                                                     TransactionBackgroundService.ExternalTransactions);

        if (isSecurity && createTransaction.ToAccountNumber is null) // Security
            return await PrepareFromAccountTransaction(createTransaction.ToPrepareFromAccountTransaction(transactionCode, fromAccount, fromCurrency),
                                                       TransactionBackgroundService.ExternalTransactions);

        if (bankCodeFrom == null || bankCodeTo == null)
            return Result.BadRequest<Transaction>("No account provided.");

        if (bankCodeFrom == Data.Bank.Code && bankCodeTo == Data.Bank.Code && Seeder.TransactionCode.TransactionCode289.Code == transactionCode.Code) // Internal Transfer
            return await PrepareInternalTransaction(createTransaction.ToPrepareInternalTransaction(transactionCode, fromAccount, fromCurrency, toAccount, toCurrency,
                                                                                                   exchangeDetails));

        return await PrepareExternalTransaction(createTransaction.ToPrepareExternalTransaction(transactionCode, fromAccount, fromCurrency, toAccount, toCurrency,
                                                                                               exchangeDetails)); // External Transfer
    }

    #region Prepare Transactions

    private async Task<Result<Transaction>> PrepareFromAccountTransaction(PrepareFromAccountTransaction prepareTransaction, ConcurrentQueue<ProcessTransaction> transactionQueue)
    {
        if (prepareTransaction.Account is null || prepareTransaction.Currency is null || prepareTransaction.Amount <= 0)
            return Result.BadRequest<Transaction>("Invalid data.");

        var transaction = prepareTransaction.ToTransaction();

        await m_TransactionRepository.Add(transaction);

        if (!prepareTransaction.Account.TryFindAccount(prepareTransaction.Currency.Id, out var accountId))
            return Result.BadRequest<Transaction>("Account does not have currency.");

        var result = await m_AccountRepository.DecreaseAvailableBalance(accountId, prepareTransaction.Currency.Id, prepareTransaction.Amount, prepareTransaction.Amount);

        if (result is not true)
        {
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

            return Result.BadRequest<Transaction>("Some error");
        }

        var processTransaction = prepareTransaction.ToProcessTransaction(transaction.Id);

        transactionQueue.Enqueue(processTransaction);

        return Result.Ok(transaction);
    }

    private async Task<Result<Transaction>> PrepareDirectFromAccountTransaction(PrepareDirectFromAccountTransaction prepareTransaction)
    {
        if (prepareTransaction.Account is null || prepareTransaction.Currency is null || prepareTransaction.Amount <= 0)
            return Result.BadRequest<Transaction>("Invalid data.");

        var transaction = prepareTransaction.ToTransaction();

        await m_TransactionRepository.Add(transaction);

        if (!prepareTransaction.Account.TryFindAccount(prepareTransaction.Currency.Id, out var accountId))
        {
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

            return Result.BadRequest<Transaction>("Account does not have currency.");
        }

        var result = await m_AccountRepository.DecreaseAvailableBalance(accountId, prepareTransaction.Amount);

        if (result is not true)
        {
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

            return Result.BadRequest<Transaction>("Some error");
        }

        var processTransaction = prepareTransaction.ToProcessTransaction(transaction.Id);

        TransactionBackgroundService.InternalTransactions.Enqueue(processTransaction);

        return Result.Ok(transaction);
    }

    private async Task<Result<Transaction>> PrepareToAccountTransaction(PrepareToAccountTransaction prepareTransaction, ConcurrentQueue<ProcessTransaction> transactionQueue)
    {
        if (prepareTransaction.Account is null || prepareTransaction.Currency is null || prepareTransaction.Amount <= 0)
            return Result.BadRequest<Transaction>("Invalid data.");

        var transaction = prepareTransaction.ToTransaction();

        await m_TransactionRepository.Add(transaction);

        if (!prepareTransaction.Account.TryFindAccount(prepareTransaction.Currency.Id, out _))
        {
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

            return Result.BadRequest<Transaction>("Account does not have currency.");
        }

        var processTransaction = prepareTransaction.ToProcessTransaction(transaction.Id);

        TransactionBackgroundService.InternalTransactions.Enqueue(processTransaction);

        return Result.Ok(transaction);
    }

    private async Task<Result<Transaction>> PrepareDirectToAccountTransaction(PrepareDirectToAccountTransaction prepareTransaction)
    {
        if (prepareTransaction.Account is null || prepareTransaction.Currency is null || prepareTransaction.Amount <= 0)
            return Result.BadRequest<Transaction>("Invalid data.");

        var transaction = prepareTransaction.ToTransaction();

        await m_TransactionRepository.Add(transaction);

        if (!prepareTransaction.Account.TryFindAccount(prepareTransaction.Currency.Id, out _))
        {
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

            return Result.BadRequest<Transaction>("Account does not have currency.");
        }

        var processTransaction = prepareTransaction.ToProcessTransaction(transaction.Id);

        TransactionBackgroundService.InternalTransactions.Enqueue(processTransaction);

        return Result.Ok(transaction);
    }

    public async Task<Result<Transaction>> PrepareInternalTransaction(PrepareInternalTransaction prepareTransaction)
    {
        if (prepareTransaction.FromAccount is null || prepareTransaction.FromCurrency is null || prepareTransaction.ToAccount is null || prepareTransaction.ToCurrency is null ||
            prepareTransaction.ExchangeDetails is null || prepareTransaction.Amount <= 0)
            return Result.BadRequest<Transaction>("Invalid data.");

        var transaction = prepareTransaction.ToTransaction();

        await m_TransactionRepository.Add(transaction);

        if (!prepareTransaction.FromAccount.TryFindAccount(prepareTransaction.FromCurrency.Id, out var accountId))
        {
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

            return Result.BadRequest<Transaction>("Account does not have currency.");
        }

        var result = await m_AccountRepository.DecreaseAvailableBalance(accountId, prepareTransaction.FromCurrency.Id, prepareTransaction.Amount,
                                                                        prepareTransaction.ExchangeDetails.ExchangeRate * prepareTransaction.ExchangeDetails.AverageRate *
                                                                        prepareTransaction.Amount);

        if (result is not true)
        {
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

            return Result.BadRequest<Transaction>("Some error");
        }

        var processTransaction = prepareTransaction.ToProcessTransaction(transaction.Id);

        TransactionBackgroundService.InternalTransactions.Enqueue(processTransaction);

        return Result.Ok(transaction);
    }

    private async Task<Result<Transaction>> PrepareExternalTransaction(PrepareExternalTransaction prepareTransaction)
    {
        var result = await CreateExternalAccounts(prepareTransaction);

        var transaction = prepareTransaction.ToTransaction();

        await m_TransactionRepository.Add(transaction);

        if (result is false)
        {
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

            return Result.BadRequest<Transaction>("Some error");
        }

        if (prepareTransaction.FromCurrency is null || prepareTransaction.ToCurrency is null || prepareTransaction.ExchangeDetails is null || prepareTransaction.Amount <= 0)
            return Result.BadRequest<Transaction>("Invalid data.");

        var bankCodeFrom = prepareTransaction.FromAccountNumber![..3];
        var bankCodeTo   = prepareTransaction.ToAccountNumber![..3];

        if (bankCodeFrom == Data.Bank.Code)
        {
            if (!prepareTransaction.FromAccount!.TryFindAccount(prepareTransaction.FromCurrency.Id, out var accountId))
            {
                await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

                return Result.BadRequest<Transaction>("Account does not have currency.");
            }

            result = await m_AccountRepository.DecreaseAvailableBalance(accountId, prepareTransaction.FromCurrency.Id, prepareTransaction.Amount,
                                                                        prepareTransaction.ExchangeDetails.ExchangeRate * prepareTransaction.ExchangeDetails.AverageRate *
                                                                        prepareTransaction.Amount);

            if (result is not true)
            {
                await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

                return Result.BadRequest<Transaction>("Some error");
            }
        }

        if (bankCodeTo == Data.Bank.Code)
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Affirm);

        var processTransaction = prepareTransaction.ToProcessTransaction(transaction.Id);

        TransactionBackgroundService.ExternalTransactions.Enqueue(processTransaction);

        return Result.Ok(transaction);
    }

    private async Task<bool> CreateExternalAccounts(PrepareExternalTransaction prepareTransaction)
    {
        if (prepareTransaction.FromAccount is null)
        {
            var accountData = await m_BankUserData.GetAccount(prepareTransaction.FromAccountNumber!);

            prepareTransaction.FromAccount = await CreateExternalAccount(accountData);
        }

        if (prepareTransaction.ToAccount is null)
        {
            var accountData = await m_BankUserData.GetAccount(prepareTransaction.ToAccountNumber!);

            prepareTransaction.ToAccount = await CreateExternalAccount(accountData);
        }

        return prepareTransaction.FromAccount is not null && prepareTransaction.ToAccount is not null;
    }

    private async Task<Account?> CreateExternalAccount(AccountResponse? accountResponse)
    {
        if (accountResponse is null)
            return null;

        var user = await m_UserRepository.Add(accountResponse.ToUser());

        return await m_AccountRepository.Add(accountResponse.ToAccount(user.Id));
    }

    #endregion

    #region Process Transactions

    public async Task ProcessInternalTransaction(ProcessTransaction processTransaction)
    {
        var isToAccountOnly   = processTransaction.FromAccountId == Guid.Empty;
        var isFromAccountOnly = processTransaction.ToAccountId   == Guid.Empty;
        var isDirect          = processTransaction.IsDirect;

        var task = (isToAccountOnly, isFromAccountOnly, isDirect) switch
                   {
                       (true, true, _)      => m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed),
                       (true, false, false) => ProcessToAccountTransaction(processTransaction),
                       (true, false, true)  => ProcessDirectToAccountTransaction(processTransaction),
                       (false, true, false) => ProcessFromAccountTransaction(processTransaction),
                       (false, true, true)  => ProcessDirectFromAccountTransaction(processTransaction),
                       (false, false, _)    => ProcessTransaction(processTransaction),
                   };

        await task;
    }

    private async Task<bool> ProcessToAccountTransaction(ProcessTransaction processTransaction)
    {
        var transactionTask = m_TransactionRepository.FindById(processTransaction.TransactionId);
        var toAccountTask   = m_AccountRepository.FindById(processTransaction.ToAccountId);

        await Task.WhenAll(transactionTask, toAccountTask);

        var transaction = await transactionTask;
        var toAccount   = await toAccountTask;

        if (transaction?.Status == TransactionStatus.Canceled)
            return true;

        if (toAccount == null || !toAccount.TryFindAccount(processTransaction.ToCurrencyId, out var accountId))
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);

            return false;
        }

        var transferSucceeded = await m_AccountRepository.IncreaseBalances(accountId, processTransaction.ToCurrencyId, processTransaction.ToAmount);

        await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, transferSucceeded ? TransactionStatus.Completed : TransactionStatus.Failed);

        return transferSucceeded;
    }

    private async Task<bool> ProcessDirectToAccountTransaction(ProcessTransaction processTransaction)
    {
        var transactionTask = m_TransactionRepository.FindById(processTransaction.TransactionId);
        var toAccountTask   = m_AccountRepository.FindById(processTransaction.ToAccountId);

        await Task.WhenAll(transactionTask, toAccountTask);

        var transaction = await transactionTask;
        var toAccount   = await toAccountTask;

        if (transaction?.Status == TransactionStatus.Canceled)
            return true;

        if (toAccount == null || !toAccount.TryFindAccount(processTransaction.ToCurrencyId, out var accountId))
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);

            return false;
        }

        var transferSucceeded = await m_AccountRepository.IncreaseBalance(accountId, processTransaction.ToAmount);

        await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, transferSucceeded ? TransactionStatus.Completed : TransactionStatus.Failed);

        return transferSucceeded;
    }

    private async Task<bool> ProcessFromAccountTransaction(ProcessTransaction processTransaction)
    {
        var transactionTask = m_TransactionRepository.FindById(processTransaction.TransactionId);
        var fromAccountTask = m_AccountRepository.FindById(processTransaction.FromAccountId);

        await Task.WhenAll(transactionTask, fromAccountTask);

        var transaction = await transactionTask;
        var fromAccount = await fromAccountTask;

        if (transaction?.Status == TransactionStatus.Canceled)
            return true;

        if (fromAccount == null || !fromAccount.TryFindAccount(processTransaction.FromCurrencyId, out var accountId))
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);

            return false;
        }

        var transferSucceeded = await m_AccountRepository.DecreaseBalance(accountId, processTransaction.FromCurrencyId, processTransaction.FromAmount,
                                                                          processTransaction.FromAmount);

        await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, transferSucceeded ? TransactionStatus.Completed : TransactionStatus.Failed);

        return transferSucceeded;
    }

    private async Task<bool> ProcessDirectFromAccountTransaction(ProcessTransaction processTransaction)
    {
        var transactionTask = m_TransactionRepository.FindById(processTransaction.TransactionId);
        var fromAccountTask = m_AccountRepository.FindById(processTransaction.FromAccountId);

        await Task.WhenAll(transactionTask, fromAccountTask);

        var transaction = await transactionTask;
        var fromAccount = await fromAccountTask;

        if (transaction?.Status == TransactionStatus.Canceled)
            return true;

        if (fromAccount == null || !fromAccount.TryFindAccount(processTransaction.FromCurrencyId, out var accountId))
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);

            return false;
        }

        var transferSucceeded = await m_AccountRepository.DecreaseBalance(accountId, processTransaction.FromAmount);

        await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, transferSucceeded ? TransactionStatus.Completed : TransactionStatus.Failed);

        return transferSucceeded;
    }

    private async Task<bool> ProcessTransaction(ProcessTransaction processTransaction)
    {
        var transactionTask = m_TransactionRepository.FindById(processTransaction.TransactionId);
        var fromAccountTask = m_AccountRepository.FindById(processTransaction.FromAccountId);
        var toAccountTask   = m_AccountRepository.FindById(processTransaction.ToAccountId);

        await Task.WhenAll(transactionTask, fromAccountTask);

        var transaction = await transactionTask;
        var fromAccount = await fromAccountTask;
        var toAccount   = await toAccountTask;

        if (transaction?.Status == TransactionStatus.Canceled)
            return true;

        if (fromAccount == null || toAccount == null || !fromAccount.TryFindAccount(processTransaction.FromCurrencyId, out var fromAccountId) ||
            !toAccount.TryFindAccount(processTransaction.ToCurrencyId, out var toAccountId))
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);

            return false;
        }

        var transferSucceeded = await m_AccountRepository.DecreaseBalance(fromAccountId, processTransaction.FromCurrencyId, processTransaction.FromAmount,
                                                                          processTransaction.FromBankAmount);

        await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, transferSucceeded ? TransactionStatus.Affirm : TransactionStatus.Failed);

        transferSucceeded = await m_AccountRepository.IncreaseBalances(toAccountId, processTransaction.ToCurrencyId, processTransaction.ToAmount);

        await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, transferSucceeded ? TransactionStatus.Completed : TransactionStatus.Failed);

        return transferSucceeded;
    }

    #endregion
}
