using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.BackgroundServices;
using Bank.UserService.Database;
using Bank.UserService.Mappers;
using Bank.UserService.Models;
using Bank.UserService.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Services;

public interface ITransactionService
{
    Task<Result<Page<TransactionResponse>>> GetAll(TransactionFilterQuery transactionFilterQuery, Pageable pageable);

    Task<Result<Page<TransactionResponse>>> GetAllByAccountId(Guid accountId, TransactionFilterQuery transactionFilterQuery, Pageable pageable);

    Task<Result<TransactionResponse>> GetOne(Guid id);

    Task<Result<TransactionCreateResponse>> Create(TransactionCreateRequest transactionCreateRequest);

    Task<Result<TransactionResponse>> Update(TransactionUpdateRequest transactionUpdateRequest, Guid id);

    Task<Result<Transaction>> PrepareInternalTransaction(PrepareInternalTransaction internalTransaction);

    Task ProcessInternalTransaction(ProcessTransaction processTransaction);
}

public class TransactionService(
    ITransactionRepository                transactionRepository,
    IAuthorizationService                 authorizationService,
    IAccountRepository                    accountRepository,
    ICurrencyRepository                   currencyRepository,
    TransactionBackgroundService          transactionBackgroundService,
    IAccountCurrencyRepository            accountCurrencyRepository,
    IExchangeService                      exchangeService,
    IDbContextFactory<ApplicationContext> contextFactory
) : ITransactionService
{
    private readonly ITransactionRepository                m_TransactionRepository        = transactionRepository;
    private readonly IAccountRepository                    m_AccountRepository            = accountRepository;
    private readonly IAccountCurrencyRepository            m_AccountCurrencyRepository    = accountCurrencyRepository;
    private readonly ICurrencyRepository                   m_CurrencyRepository           = currencyRepository;
    private readonly IAuthorizationService                 m_AuthorizationService         = authorizationService;
    private readonly IExchangeService                      m_ExchangeService              = exchangeService;
    private readonly TransactionBackgroundService          m_TransactionBackgroundService = transactionBackgroundService;
    private readonly IDbContextFactory<ApplicationContext> m_ContextFactory               = contextFactory;

    private Task<ApplicationContext> CreateContext => m_ContextFactory.CreateDbContextAsync();

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

        if (m_AuthorizationService.Role     == Role.Client && transaction.FromAccount?.ClientId != m_AuthorizationService.UserId &&
            transaction.ToAccount?.ClientId != m_AuthorizationService.UserId)
            return Result.Unauthorized<TransactionResponse>();

        return Result.Ok(transaction.ToResponse());
    }

    public async Task<Result<Page<TransactionResponse>>> GetAllByAccountId(Guid accountId, TransactionFilterQuery transactionFilterQuery, Pageable pageable)
    {
        var page = await m_TransactionRepository.FindAllByAccountId(accountId, transactionFilterQuery, pageable);

        if (m_AuthorizationService.Role == Role.Client &&
            page.Items.Any(transaction => transaction.FromAccount!.ClientId != m_AuthorizationService.UserId && transaction.ToAccount!.ClientId != m_AuthorizationService.UserId))
            return Result.Forbidden<Page<TransactionResponse>>();

        var transactionResponses = page.Items.Select(transaction => transaction.ToResponse())
                                       .ToList();

        return Result.Ok(new Page<TransactionResponse>(transactionResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<TransactionCreateResponse>> Create(TransactionCreateRequest transactionCreateRequest)
    {
        var transaction = await CreateTransaction(new TempyTransaction
                                                  {
                                                      FromAccountNumber = transactionCreateRequest.FromAccountNumber,
                                                      FromCurrencyId    = transactionCreateRequest.FromCurrencyId,
                                                      Amount            = transactionCreateRequest.Amount,
                                                      ToAccountNumber   = transactionCreateRequest.ToAccountNumber,
                                                      ToCurrencyId      = transactionCreateRequest.ToCurrencyId,
                                                      Purpose           = transactionCreateRequest.Purpose,
                                                      ReferenceNumber   = transactionCreateRequest.ReferenceNumber,
                                                      CodeId            = transactionCreateRequest.CodeId,
                                                  });

        if (transaction.Value == null)
            return Result.BadRequest<TransactionCreateResponse>("Not going thru.");

        return Result.Ok(transaction.Value.ToCreateResponse());
    }

    public async Task<Result<TransactionResponse>> Update(TransactionUpdateRequest request, Guid id) 
    {
        var dbTransaction = await m_TransactionRepository.FindById(id);

        if (dbTransaction is null)
            return Result.NotFound<TransactionResponse>($"No Transaction found with Id: {id}");

        var transaction = await m_TransactionRepository.Update(dbTransaction.ToTransaction(request));

        return Result.Ok(transaction.ToResponse());
    }

    public async Task<Result<Transaction>> CreateTransaction(TempyTransaction tempyTransaction)
    {
        if (tempyTransaction.FromAccountNumber == null && tempyTransaction.ToAccountNumber == null)
            return Result.BadRequest<Transaction>("No valid account provided.");

        var fromAccount     = await m_AccountRepository.FindByNumber(tempyTransaction.FromAccountNumber?.Substring(7, 9) ?? "");
        var toAccount       = await m_AccountRepository.FindByNumber(tempyTransaction.ToAccountNumber?.Substring(7, 9)   ?? "");
        var exchangeDetails = await m_ExchangeService.CalculateExchangeDetails(tempyTransaction.FromCurrencyId, tempyTransaction.ToCurrencyId);

        var bankCodeFrom = tempyTransaction.FromAccountNumber?[..3];
        var bankCodeTo   = tempyTransaction.ToAccountNumber?[..3];

        if (bankCodeTo == null && bankCodeFrom == m_TransactionBackgroundService.Bank.Code && fromAccount != null)
            return await PrepareWithdrawTransaction(new PrepareWithdrawTransaction
                                                    {
                                                        Account    = fromAccount,
                                                        CurrencyId = tempyTransaction.FromCurrencyId,
                                                        Amount     = tempyTransaction.Amount
                                                    });

        if (bankCodeFrom == null && bankCodeTo == m_TransactionBackgroundService.Bank.Code && toAccount != null)
            return await PrepareDepositTransaction(new PrepareDepositTransaction
                                                   {
                                                       Account    = toAccount,
                                                       CurrencyId = tempyTransaction.ToCurrencyId,
                                                       Amount     = tempyTransaction.Amount
                                                   });

        if (bankCodeFrom == null || bankCodeTo == null || exchangeDetails == null)
            return Result.BadRequest<Transaction>("Some error");

        if (bankCodeFrom == m_TransactionBackgroundService.Bank.Code && bankCodeTo == m_TransactionBackgroundService.Bank.Code && fromAccount != null && toAccount != null)
            return await PrepareInternalTransaction(new PrepareInternalTransaction
                                                    {
                                                        FromAccount       = fromAccount,
                                                        FromCurrencyId    = tempyTransaction.FromCurrencyId,
                                                        ToAccount         = toAccount,
                                                        ToCurrencyId      = tempyTransaction.ToCurrencyId,
                                                        Amount            = tempyTransaction.Amount,
                                                        TransactionCodeId = tempyTransaction.CodeId,
                                                        ReferenceNumber   = tempyTransaction.ReferenceNumber,
                                                        Purpose           = tempyTransaction.Purpose,
                                                        ExchangeDetails   = exchangeDetails
                                                    });

        return await AddExternalTransaction(tempyTransaction);
    }

    private async Task<Result<Transaction>> PrepareDepositTransaction(PrepareDepositTransaction depositTransaction)
    {
        var currencyExists = await m_CurrencyRepository.Exists(depositTransaction.CurrencyId);

        if (!currencyExists || depositTransaction.Amount <= 0)
            return Result.BadRequest<Transaction>("Some error");

        var transaction = depositTransaction.ToTransaction();

        await m_TransactionRepository.Add(transaction);

        var processTransaction = depositTransaction.ToProcessTransaction(transaction.Id);

        m_TransactionBackgroundService.InternalTransactions.Enqueue(processTransaction);

        return Result.Ok(transaction);
    }

    private async Task<Result<Transaction>> PrepareWithdrawTransaction(PrepareWithdrawTransaction withdrawTransaction)
    {
        var currencyExists = await m_CurrencyRepository.Exists(withdrawTransaction.CurrencyId);

        if (!currencyExists || withdrawTransaction.Amount <= 0)
            return Result.BadRequest<Transaction>("Some error");

        var transaction = withdrawTransaction.ToTransaction();

        await m_TransactionRepository.Add(transaction);

        withdrawTransaction.Account.TryFindAccount(withdrawTransaction.CurrencyId, out var accountId);

        var result = await m_AccountRepository.DecreaseAvailableBalance(accountId, withdrawTransaction.CurrencyId, withdrawTransaction.Amount, withdrawTransaction.Amount);

        if (result is not true)
        {
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

            return Result.BadRequest<Transaction>("Some error");
        }

        var processTransaction = withdrawTransaction.ToProcessTransaction(transaction.Id);

        m_TransactionBackgroundService.InternalTransactions.Enqueue(processTransaction);

        return Result.Ok(transaction);
    }

    public async Task<Result<Transaction>> PrepareInternalTransaction(PrepareInternalTransaction internalTransaction)
    {
        var currencyExistResults = await Task.WhenAll(m_CurrencyRepository.Exists(internalTransaction.FromCurrencyId),
                                                      m_CurrencyRepository.Exists(internalTransaction.ToCurrencyId));

        if (currencyExistResults.Any(result => !result) || internalTransaction.Amount <= 0)
            return Result.BadRequest<Transaction>("Some error");

        var transaction = internalTransaction.ToTransaction();

        await m_TransactionRepository.Add(transaction);

        internalTransaction.FromAccount.TryFindAccount(internalTransaction.FromCurrencyId, out var accountId);

        var result = await m_AccountRepository.DecreaseAvailableBalance(accountId, internalTransaction.FromCurrencyId, internalTransaction.Amount,
                                                                        internalTransaction.ExchangeDetails.ExchangeRate * internalTransaction.ExchangeDetails.AverageRate *
                                                                        internalTransaction.Amount);

        if (result is not true)
        {
            await m_TransactionRepository.UpdateStatus(transaction.Id, TransactionStatus.Failed);

            return Result.BadRequest<Transaction>("Some error");
        }

        var processTransaction = internalTransaction.ToProcessTransaction(transaction.Id);

        m_TransactionBackgroundService.InternalTransactions.Enqueue(processTransaction);

        return Result.Ok(transaction);
    }

    private async Task<Result<Transaction>> AddExternalTransaction(TempyTransaction tempyTransaction) // TODO: external transaction 
    {
        return Result.Forbidden<Transaction>();
    }

    #region Process Transactions

    public async Task ProcessInternalTransaction(ProcessTransaction processTransaction)
    {
        var isDeposit  = processTransaction.FromAccountId == Guid.Empty;
        var isWithdraw = processTransaction.ToAccountId   == Guid.Empty;

        if (isDeposit && isWithdraw)
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);
            return;
        }

        var task = (isDeposit, isWithdraw) switch
                   {
                       (true, true)   => m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed),
                       (true, false)  => ProcessDepositTransaction(processTransaction),
                       (false, true)  => ProcessWithdrawTransaction(processTransaction),
                       (false, false) => ProcessTransaction(processTransaction),
                   };

        await task;
    }

    private async Task<bool> ProcessDepositTransaction(ProcessTransaction processTransaction)
    {
        await using var databaseContext     = await CreateContext;
        await using var databaseTransaction = await databaseContext.Database.BeginTransactionAsync();

        var transactionTask = m_TransactionRepository.FindById(processTransaction.TransactionId);
        var toAccountTask   = m_AccountRepository.FindById(processTransaction.ToAccountId);

        await Task.WhenAll(transactionTask, toAccountTask);

        var transaction = await transactionTask;
        var toAccount   = await toAccountTask;

        if (transaction?.Status == TransactionStatus.Canceled)
            return true;

        if (toAccount == null)
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);

            return false;
        }

        toAccount.TryFindAccount(processTransaction.ToCurrencyId, out var accountId);

        var transferSucceeded = await m_AccountRepository.IncreaseBalances(accountId, processTransaction.ToCurrencyId, processTransaction.ToAmount, databaseContext);

        if (!transferSucceeded)
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);
            await databaseTransaction.RollbackAsync();

            return false;
        }

        await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Completed);
        await databaseTransaction.CommitAsync();

        return true;
    }

    private async Task<bool> ProcessWithdrawTransaction(ProcessTransaction processTransaction)
    {
        await using var databaseContext     = await CreateContext;
        await using var databaseTransaction = await databaseContext.Database.BeginTransactionAsync();

        var transactionTask = m_TransactionRepository.FindById(processTransaction.TransactionId);
        var fromAccountTask = m_AccountRepository.FindById(processTransaction.FromAccountId);

        await Task.WhenAll(transactionTask, fromAccountTask);

        var transaction = await transactionTask;
        var fromAccount = await fromAccountTask;

        if (transaction?.Status == TransactionStatus.Canceled)
            return true;

        if (fromAccount == null)
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);

            return false;
        }

        fromAccount.TryFindAccount(processTransaction.FromCurrencyId, out var accountId);

        var transferSucceeded = await m_AccountRepository.DecreaseAvailableBalance(accountId, processTransaction.FromCurrencyId, processTransaction.FromAmount,
                                                                                   processTransaction.FromAmount, databaseContext);

        if (!transferSucceeded)
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);
            await databaseTransaction.RollbackAsync();

            return false;
        }

        await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Completed);
        await databaseTransaction.CommitAsync();

        return true;
    }

    private async Task<bool> ProcessTransaction(ProcessTransaction processTransaction)
    {
        await using var databaseContext     = await CreateContext;
        await using var databaseTransaction = await databaseContext.Database.BeginTransactionAsync();

        var transferSucceeded = true;

        var transactionTask = m_TransactionRepository.FindById(processTransaction.TransactionId);
        var fromAccountTask = m_AccountRepository.FindById(processTransaction.FromAccountId);
        var toAccountTask   = m_AccountRepository.FindById(processTransaction.ToAccountId);

        await Task.WhenAll(transactionTask, fromAccountTask);

        var transaction = await transactionTask;
        var fromAccount = await fromAccountTask;
        var toAccount   = await toAccountTask;

        if (transaction?.Status == TransactionStatus.Canceled)
            return true;

        if (fromAccount == null || toAccount == null)
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);

            return false;
        }

        fromAccount.TryFindAccount(processTransaction.FromCurrencyId, out var fromAccountId);

        transferSucceeded = await m_AccountRepository.DecreaseBalance(fromAccountId, processTransaction.FromCurrencyId, processTransaction.FromAmount,
                                                                      processTransaction.FromBankAmount, databaseContext);

        if (!transferSucceeded)
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);
            await databaseTransaction.RollbackAsync();

            return false;
        }

        await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Affirm);

        toAccount.TryFindAccount(processTransaction.ToCurrencyId, out var toAccountId);

        transferSucceeded = await m_AccountRepository.IncreaseBalances(toAccountId, processTransaction.ToCurrencyId, processTransaction.ToAmount, databaseContext);

        if (!transferSucceeded)
        {
            await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Failed);
            await databaseTransaction.RollbackAsync();

            return false;
        }

        await m_TransactionRepository.UpdateStatus(processTransaction.TransactionId, TransactionStatus.Completed);
        await databaseTransaction.CommitAsync();

        return true;
    }

    #endregion
}
