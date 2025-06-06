using System.Transactions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;
using Bank.UserService.Repositories;
using Bank.UserService.Services;

using Transaction = Bank.UserService.Models.Transaction;
using TransactionStatus = Bank.Application.Domain.TransactionStatus;

namespace Bank.UserService.HostedServices;

public class LoanHostedService
{
    private readonly Random                     m_Random = new();
    private readonly ILoanRepository            m_LoanRepository;
    private readonly IAccountRepository         m_AccountRepository;
    private readonly IInstallmentRepository     m_InstallmentRepository;
    private readonly IEmailService              m_EmailService;
    private readonly ITransactionCodeRepository m_TransactionCodeRepository;
    private readonly IExchangeService           m_ExchangeService;
    private readonly ITransactionService        m_transactionService;
    private          Timer?                     m_Timer;

    public LoanHostedService(ILoanRepository loanRepository, IAccountRepository accountRepository, IInstallmentRepository installmentRepository, IEmailService emailService,
                             ITransactionCodeRepository transactionCodeRepository, IExchangeService exchangeService, ITransactionService transactionService)
    {
        m_LoanRepository            = loanRepository;
        m_AccountRepository         = accountRepository;
        m_InstallmentRepository     = installmentRepository;
        m_EmailService              = emailService;
        m_TransactionCodeRepository = transactionCodeRepository;
        m_ExchangeService           = exchangeService;
        m_transactionService        = transactionService;
    }

    public void OnApplicationStarted()
    {
        Initialize();
    }

    public void OnApplicationStopped()
    {
        m_Timer?.Dispose();
    }

    private void Initialize()
    {
        var midnight          = DateTime.Today.AddDays(1);
        var timeLeftUntilNext = midnight.Subtract(DateTime.UtcNow);

        m_Timer = new Timer(async _ => await ProcessLoanPayments(), null, timeLeftUntilNext, TimeSpan.FromDays(1));
    }

    public async Task ProcessLoanPayments()
    {
        try
        {
            var today = DateTime.UtcNow.Date;

            var activeLoans = await m_LoanRepository.GetLoansWithDueInstallmentsAsync(today);

            foreach (var loan in activeLoans)
                await ProcessLoanInstallmentsAsync(loan, today, m_LoanRepository, m_AccountRepository, m_InstallmentRepository);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task ProcessLoanInstallmentsAsync(Loan                   loan, DateTime currentDate, ILoanRepository loanRepository, IAccountRepository accountRepository,
                                                   IInstallmentRepository installmentRepository)
    {
        try
        {
            var dueInstallments = await installmentRepository.GetDueInstallmentsForLoanAsync(loan.Id, currentDate);

            if (dueInstallments.Count == 0)
                return;

            foreach (var installment in dueInstallments)
            {
                var paymentAmount = await CalculateInstallmentAmount(loan);

                // Process payment
                var paymentSuccessful = await ProcessPaymentAsync(loan, paymentAmount, accountRepository);

                if (paymentSuccessful)
                {
                    var updatedInstallment = new Installment
                                             {
                                                 Id              = installment.Id,
                                                 LoanId          = installment.LoanId,
                                                 InterestRate    = installment.InterestRate,
                                                 ExpectedDueDate = installment.ExpectedDueDate,
                                                 ActualDueDate   = currentDate,
                                                 Status          = InstallmentStatus.Paid,
                                                 CreatedAt       = installment.CreatedAt,
                                                 ModifiedAt      = DateTime.UtcNow
                                             };

                    await installmentRepository.Update(installment, updatedInstallment);

                    var client = await GetClientByLoan(loan, accountRepository);

                    if (client != null)
                    {
                        // Izračunaj preostali dug
                        var remainingBalance  = await GetRemainingPrincipal(loan, installmentRepository);
                        var installmentAmount = await CalculateInstallmentAmount(loan);
                        await m_EmailService.Send(EmailType.LoanInstallmentPaid, client, client.FirstName, installmentAmount, loan.Currency.Code, remainingBalance);
                    }

                    await CreateNextInstallmentIfNeededAsync(loan, installmentRepository);
                }
            }

            await CheckLoanCompletionAsync(loan, loanRepository, installmentRepository);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ProcessPaymentAsync(Loan loan, decimal paymentAmount, IAccountRepository accountRepository)
    {
        try
        {
            var account = await accountRepository.FindById(loan.AccountId);

            if (account == null)
            {
                return false;
            }

            // Create a transaction request for loan payment
            var bankAccount = await m_AccountRepository.FindById(Seeder.Account.BankAccount.Id);

            var transactionRequest = new TransactionCreateRequest
                                     {
                                         FromAccountNumber = account.AccountNumber,
                                         ToAccountNumber   = bankAccount!.AccountNumber,
                                         FromCurrencyId    = account.CurrencyId,
                                         ToCurrencyId      = loan.CurrencyId,
                                         Amount            = paymentAmount,
                                         CodeId            = Seeder.TransactionCode.TransactionCode276.Id,
                                         Purpose           = "loan payment"
                                     };

            var result = await m_transactionService.Create(transactionRequest);

            return result.Value != null;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task CreateNextInstallmentIfNeededAsync(Loan loan, IInstallmentRepository installmentRepository)
    {
        var totalInstallments = await installmentRepository.GetInstallmentCountForLoanAsync(loan.Id);

        if (totalInstallments < loan.Period)
        {
            var latestInstallment = await installmentRepository.GetLatestInstallmentForLoanAsync(loan.Id);

            var newInstallment = new Installment
                                 {
                                     Id              = Guid.NewGuid(),
                                     LoanId          = loan.Id,
                                     InterestRate    = await GetEffectiveInterestRate(loan),
                                     ExpectedDueDate = latestInstallment!.ExpectedDueDate.AddMonths(1),
                                     Status          = InstallmentStatus.Pending,
                                     CreatedAt       = DateTime.UtcNow,
                                     ModifiedAt      = DateTime.UtcNow
                                 };

            await installmentRepository.Add(newInstallment);
        }
    }

    public async Task CheckLoanCompletionAsync(Loan loan, ILoanRepository loanRepository, IInstallmentRepository installmentRepository)
    {
        var allPaid           = await installmentRepository.AreAllInstallmentsPaidAsync(loan.Id);
        var totalInstallments = await installmentRepository.GetInstallmentCountForLoanAsync(loan.Id);

        if (allPaid && totalInstallments >= loan.Period)
        {
            var updatedLoan = new Loan
                              {
                                  Id           = loan.Id,
                                  TypeId       = loan.TypeId,
                                  AccountId    = loan.AccountId,
                                  Amount       = loan.Amount,
                                  Period       = loan.Period,
                                  CreationDate = loan.CreationDate,
                                  MaturityDate = loan.MaturityDate,
                                  CurrencyId   = loan.CurrencyId,
                                  Status       = LoanStatus.Closed,
                                  InterestType = loan.InterestType,
                                  CreatedAt    = loan.CreatedAt,
                                  ModifiedAt   = DateTime.UtcNow
                              };

            await loanRepository.Update(updatedLoan);
        }
    }

    public async Task<decimal> CalculateInstallmentAmount(Loan loan)
    {
        var loanAmountInRsd = await ConvertToRsd(loan.Amount, loan.Currency!);

        var monthlyPayment = CalculateMonthlyPayment(loanAmountInRsd, loan.Period, await GetEffectiveInterestRate(loan));

        return monthlyPayment;
    }

    public async Task<decimal> GetEffectiveInterestRate(Loan loan)
    {
        var amountInRsd   = await ConvertToRsd(loan.Amount, loan.Currency!);
        var effectiveRate = GetBaseInterestRate(amountInRsd);

        if (loan.InterestType == InterestType.Variable)
        {
            effectiveRate += loan.LoanType!.Margin;

            effectiveRate += GetCurrentEuriborRate();

            effectiveRate /= 12;
        }

        return effectiveRate;
    }

    public async Task<decimal> GetRemainingPrincipal(Loan loan, IInstallmentRepository installmentRepository)
    {
        var latestInstallment = await installmentRepository.GetLatestInstallmentForLoanAsync(loan.Id);

        if (latestInstallment == null)
            return loan.Amount;

        var paidInstallments = await installmentRepository.GetPaidInstallmentsCountBeforeDateAsync(loan.Id, latestInstallment.ExpectedDueDate);

        if (paidInstallments == 0)
            return loan.Amount;

        var loanAmountInRsd    = await ConvertToRsd(loan.Amount, loan.Currency);
        var remainingPrincipal = loanAmountInRsd;

        var baseInterestRate = GetBaseInterestRate(loanAmountInRsd);

        for (var i = 0; i < paidInstallments; i++)
        {
            var effectiveRate = baseInterestRate;

            if (loan.InterestType == InterestType.Variable)
            {
                effectiveRate += loan.LoanType!.Margin;

                if (loan.Currency.Code != "RSD")
                    effectiveRate += GetCurrentEuriborRate();
            }

            var monthlyRate = effectiveRate / 1200;

            var interestPayment = remainingPrincipal * monthlyRate;

            var monthlyPayment = CalculateMonthlyPayment(loanAmountInRsd, loan.Period, effectiveRate);

            var principalPayment = monthlyPayment - interestPayment;

            remainingPrincipal -= principalPayment;
        }

        return remainingPrincipal;
    }

    public decimal GetBaseInterestRate(decimal amountInRsd)
    {
        if (amountInRsd <= 500000) return 6.25m;
        if (amountInRsd <= 1000000) return 6.00m;
        if (amountInRsd <= 2000000) return 5.75m;
        if (amountInRsd <= 5000000) return 5.50m;
        if (amountInRsd <= 10000000) return 5.25m;
        if (amountInRsd <= 20000000) return 5.00m;
        return 4.75m;
    }

    public decimal GetCurrentEuriborRate()
    {
        return (decimal)(m_Random.NextDouble() * 3.0 - 1.5);
    }

    public decimal CalculateMonthlyPayment(decimal loanAmount, int loanPeriodMonths, decimal annualInterestRate)
    {
        var monthlyRate = annualInterestRate / 1200;

        if (monthlyRate == 0)
            return loanAmount / loanPeriodMonths;

        var pow = Math.Pow(1 + (double)monthlyRate, loanPeriodMonths);
        return loanAmount * monthlyRate * (decimal)pow / ((decimal)pow - 1);
    }

    public async Task<decimal> ConvertToRsd(decimal amount, Currency currency)
    {
        if (currency.Code == "RSD")
            return amount;

        var exchangeBetweenQuery = new ExchangeBetweenQuery
                                   {
                                       CurrencyFromCode = currency.Code,
                                       CurrencyToCode   = "RSD"
                                   };

        var result = await m_ExchangeService.GetByCurrencies(exchangeBetweenQuery);

        var convertedAmount = amount * result.Value!.Rate;

        return convertedAmount;
    }

    private async Task<User?> GetClientByLoan(Loan loan, IAccountRepository accountRepository)
    {
        var account = await accountRepository.FindById(loan.AccountId);
        return account?.Client;
    }

    public async Task<bool> DisperseFundsAfterLoanActivation(Loan loan)
    {
        try
        {
            // Get the client's account
            var account = await m_AccountRepository.FindById(loan.AccountId);

            if (account == null)
            {
                return false;
            }

            var bankAccount = await m_AccountRepository.FindById(Seeder.Account.BankAccount.Id);

            // Create a transaction request for loan disbursement
            var transactionRequest = new TransactionCreateRequest
                                     {
                                         FromAccountNumber = bankAccount!.AccountNumber, // Bank is the source, can be empty for deposits
                                         ToAccountNumber   = account.AccountNumber,
                                         ToCurrencyId      = loan.CurrencyId,
                                         Amount            = loan.Amount,
                                         CodeId            = Seeder.TransactionCode.TransactionCode270.Id, // Loan disbursement code
                                         ReferenceNumber   = "12345",
                                         Purpose           = "Loan disbursement"
                                     };

            // Use TransactionService to create the transaction
            await m_transactionService.Create(transactionRequest);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
