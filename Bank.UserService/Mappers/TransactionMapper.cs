using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class TransactionMapper
{
    public static TransactionResponse ToResponse(this Transaction transaction)
    {
        return new TransactionResponse
               {
                   Id              = transaction.Id,
                   FromAccount     = transaction.FromAccount!.ToSimpleResponse(),
                   FromCurrency    = transaction.FromCurrency!.ToResponse(),
                   ToAccount       = transaction.ToAccount!.ToSimpleResponse(),
                   ToCurrency      = transaction.ToCurrency!.ToResponse(),
                   FromAmount      = transaction.FromAmount,
                   ToAmount        = transaction.ToAmount,
                   Code            = transaction.Code?.ToResponse()!,
                   ReferenceNumber = transaction.ReferenceNumber!,
                   Purpose         = transaction.Purpose ?? "",
                   Status          = transaction.Status,
                   CreatedAt       = transaction.CreatedAt,
                   ModifiedAt      = transaction.ModifiedAt,
               };
    }

    public static Transaction Update(this Transaction transaction, TransactionUpdateRequest transactionUpdateRequest)
    {
        transaction.Status     = transactionUpdateRequest.Status;
        transaction.ModifiedAt = DateTime.UtcNow;
        return transaction;
    }

    #region Create Transaction

    public static PrepareFromAccountTransaction ToPrepareFromAccountTransaction(this TransactionCreateRequest createRequest, TransactionCode transactionCode, Account? account,
                                                                                Currency?                     currency)
    {
        return new PrepareFromAccountTransaction
               {
                   Account         = account,
                   Currency        = currency,
                   TransactionCode = transactionCode,
                   Amount          = createRequest.Amount
               };
    }

    public static PrepareDirectFromAccountTransaction ToPrepareDirectFromAccountTransaction(this TransactionCreateRequest createRequest, TransactionCode transactionCode,
                                                                                            Account?                      account,       Currency?       currency)
    {
        return new PrepareDirectFromAccountTransaction
               {
                   Account         = account,
                   Currency        = currency,
                   TransactionCode = transactionCode,
                   Amount          = createRequest.Amount
               };
    }

    public static PrepareToAccountTransaction ToPrepareToAccountTransaction(this TransactionCreateRequest createRequest, TransactionCode transactionCode, Account? account,
                                                                            Currency?                     currency)
    {
        return new PrepareToAccountTransaction
               {
                   Account         = account,
                   Currency        = currency,
                   TransactionCode = transactionCode,
                   Amount          = createRequest.Amount
               };
    }

    public static PrepareDirectToAccountTransaction ToPrepareDirectToAccountTransaction(this TransactionCreateRequest createRequest, TransactionCode transactionCode,
                                                                                        Account?                      account,       Currency?       currency)
    {
        return new PrepareDirectToAccountTransaction
               {
                   Account         = account,
                   Currency        = currency,
                   TransactionCode = transactionCode,
                   Amount          = createRequest.Amount
               };
    }

    public static PrepareInternalTransaction ToPrepareInternalTransaction(this TransactionCreateRequest createRequest, TransactionCode transactionCode, Account? fromAccount,
                                                                          Currency? fromCurrency, Account? toAccount, Currency? toCurrency, ExchangeDetails? exchangeDetails)
    {
        return new PrepareInternalTransaction
               {
                   FromAccount     = fromAccount,
                   FromCurrency    = fromCurrency,
                   ToAccount       = toAccount,
                   ToCurrency      = toCurrency,
                   Amount          = createRequest.Amount,
                   TransactionCode = transactionCode,
                   ExchangeDetails = exchangeDetails,
                   ReferenceNumber = createRequest.ReferenceNumber,
                   Purpose         = createRequest.Purpose,
               };
    }

    public static PrepareExternalTransaction ToPrepareExternalTransaction(this TransactionCreateRequest createRequest, TransactionCode transactionCode, Account? fromAccount,
                                                                          Currency? fromCurrency, Account? toAccount, Currency? toCurrency, ExchangeDetails? exchangeDetails)
    {
        return new PrepareExternalTransaction
               {
                   FromAccountNumber     = createRequest.FromAccountNumber ?? string.Empty,
                   FromAccount           = fromAccount,
                   FromCurrency          = fromCurrency,
                   ToAccountNumber       = createRequest.ToAccountNumber ?? string.Empty,
                   ToAccount             = toAccount,
                   ToCurrency            = toCurrency,
                   Amount                = createRequest.Amount,
                   TransactionCode       = transactionCode,
                   ExchangeDetails       = exchangeDetails,
                   ReferenceNumber       = createRequest.ReferenceNumber,
                   Purpose               = createRequest.Purpose,
                   ExternalTransactionId = createRequest.ExternalTransactionId
               };
    }

    #endregion

    #region To Transaction

    public static Transaction ToTransaction(this PrepareFromAccountTransaction fromAccountTransaction)
    {
        return new Transaction
               {
                   Id             = Guid.NewGuid(),
                   FromAccountId  = fromAccountTransaction.Account!.Id,
                   FromCurrencyId = fromAccountTransaction.Currency!.Id,
                   FromAmount     = fromAccountTransaction.Amount,
                   CodeId         = Seeder.TransactionCode.TransactionCode266.Id,
                   Status         = TransactionStatus.Pending,
                   CreatedAt      = DateTime.UtcNow,
                   ModifiedAt     = DateTime.UtcNow
               };
    }

    public static Transaction ToTransaction(this PrepareDirectFromAccountTransaction fromAccountTransaction)
    {
        return new Transaction
               {
                   Id             = Guid.NewGuid(),
                   FromAccountId  = fromAccountTransaction.Account!.Id,
                   FromCurrencyId = fromAccountTransaction.Currency!.Id,
                   FromAmount     = fromAccountTransaction.Amount,
                   CodeId         = Seeder.TransactionCode.TransactionCode266.Id,
                   Status         = TransactionStatus.Pending,
                   CreatedAt      = DateTime.UtcNow,
                   ModifiedAt     = DateTime.UtcNow
               };
    }

    public static Transaction ToTransaction(this PrepareToAccountTransaction toAccountTransaction)
    {
        return new Transaction
               {
                   Id           = Guid.NewGuid(),
                   ToAccountId  = toAccountTransaction.Account!.Id,
                   ToCurrencyId = toAccountTransaction.Currency!.Id,
                   ToAmount     = toAccountTransaction.Amount,
                   CodeId       = Seeder.TransactionCode.TransactionCode289.Id,
                   Status       = TransactionStatus.Pending,
                   CreatedAt    = DateTime.UtcNow,
                   ModifiedAt   = DateTime.UtcNow
               };
    }

    public static Transaction ToTransaction(this PrepareDirectToAccountTransaction toAccountTransaction)
    {
        return new Transaction
               {
                   Id           = Guid.NewGuid(),
                   ToAccountId  = toAccountTransaction.Account!.Id,
                   ToCurrencyId = toAccountTransaction.Currency!.Id,
                   ToAmount     = toAccountTransaction.Amount,
                   CodeId       = Seeder.TransactionCode.TransactionCode289.Id,
                   Status       = TransactionStatus.Pending,
                   CreatedAt    = DateTime.UtcNow,
                   ModifiedAt   = DateTime.UtcNow
               };
    }

    public static Transaction ToTransaction(this PrepareInternalTransaction internalTransaction)
    {
        return new Transaction
               {
                   Id              = Guid.NewGuid(),
                   FromAccountId   = internalTransaction.FromAccount!.Id,
                   FromCurrencyId  = internalTransaction.FromCurrency!.Id,
                   FromAmount      = internalTransaction.Amount,
                   ToAccountId     = internalTransaction.ToAccount!.Id,
                   ToCurrencyId    = internalTransaction.ToCurrency!.Id,
                   ToAmount        = internalTransaction.Amount * internalTransaction.ExchangeDetails!.ExchangeRate,
                   CodeId          = internalTransaction.TransactionCode.Id,
                   Status          = TransactionStatus.Pending,
                   Purpose         = internalTransaction.Purpose,
                   ReferenceNumber = internalTransaction.ReferenceNumber,
                   CreatedAt       = DateTime.UtcNow,
                   ModifiedAt      = DateTime.UtcNow
               };
    }

    public static Transaction ToTransaction(this PrepareExternalTransaction externalTransaction) //TODO Check amounts and status
    {
        var isIncoming = externalTransaction.FromAccount!.AccountNumber[..3] == Seeder.Bank.Bank02.Code;

        return new Transaction
               {
                   Id              = Guid.NewGuid(),
                   FromAccountId   = externalTransaction.FromAccount!.Id,
                   FromCurrencyId  = externalTransaction.FromCurrency!.Id,
                   FromAmount      = externalTransaction.Amount,
                   ToAccountId     = externalTransaction.ToAccount!.Id,
                   ToCurrencyId    = externalTransaction.ToCurrency!.Id,
                   ToAmount        = externalTransaction.Amount * (isIncoming ? 1 : externalTransaction.ExchangeDetails!.ExchangeRate),
                   CodeId          = externalTransaction.TransactionCode.Id,
                   Status          = isIncoming ? TransactionStatus.Affirm : TransactionStatus.Pending,
                   Purpose         = externalTransaction.Purpose,
                   ReferenceNumber = externalTransaction.ReferenceNumber,
                   CreatedAt       = DateTime.UtcNow,
                   ModifiedAt      = DateTime.UtcNow
               };
    }

    #endregion

    #region To Process Transaction

    public static ProcessTransaction ToProcessTransaction(this PrepareFromAccountTransaction fromAccountTransaction, Guid transactionId)
    {
        return new ProcessTransaction
               {
                   TransactionId  = transactionId,
                   FromAccountId  = fromAccountTransaction.Account!.Id,
                   FromCurrencyId = fromAccountTransaction.Currency!.Id,
                   FromAmount     = fromAccountTransaction.Amount,
                   ToAccountId    = Guid.Empty,
                   ToCurrencyId   = Guid.Empty,
                   ToAmount       = 0,
                   FromBankAmount = fromAccountTransaction.Amount,
                   IsDirect       = false
               };
    }

    public static ProcessTransaction ToProcessTransaction(this PrepareDirectFromAccountTransaction fromAccountTransaction, Guid transactionId)
    {
        return new ProcessTransaction
               {
                   TransactionId  = transactionId,
                   FromAccountId  = fromAccountTransaction.Account!.Id,
                   FromCurrencyId = fromAccountTransaction.Currency!.Id,
                   FromAmount     = fromAccountTransaction.Amount,
                   ToAccountId    = Guid.Empty,
                   ToCurrencyId   = Guid.Empty,
                   ToAmount       = 0,
                   FromBankAmount = fromAccountTransaction.Amount,
                   IsDirect       = true
               };
    }

    public static ProcessTransaction ToProcessTransaction(this PrepareToAccountTransaction toAccountTransaction, Guid transactionId)
    {
        return new ProcessTransaction
               {
                   TransactionId  = transactionId,
                   FromAccountId  = Guid.Empty,
                   FromCurrencyId = Guid.Empty,
                   FromAmount     = 0,
                   ToAccountId    = toAccountTransaction.Account!.Id,
                   ToCurrencyId   = toAccountTransaction.Currency!.Id,
                   ToAmount       = toAccountTransaction.Amount,
                   FromBankAmount = 0,
                   IsDirect       = false
               };
    }

    public static ProcessTransaction ToProcessTransaction(this PrepareDirectToAccountTransaction toAccountTransaction, Guid transactionId)
    {
        return new ProcessTransaction
               {
                   TransactionId  = transactionId,
                   FromAccountId  = Guid.Empty,
                   FromCurrencyId = Guid.Empty,
                   FromAmount     = 0,
                   ToAccountId    = toAccountTransaction.Account!.Id,
                   ToCurrencyId   = toAccountTransaction.Currency!.Id,
                   ToAmount       = toAccountTransaction.Amount,
                   FromBankAmount = 0,
                   IsDirect       = true
               };
    }

    public static ProcessTransaction ToProcessTransaction(this PrepareInternalTransaction internalTransaction, Guid transactionId)
    {
        return new ProcessTransaction
               {
                   TransactionId  = transactionId,
                   FromAccountId  = internalTransaction.FromAccount!.Id,
                   FromCurrencyId = internalTransaction.FromCurrency!.Id,
                   FromAmount     = internalTransaction.Amount,
                   ToAccountId    = internalTransaction.ToAccount!.Id,
                   ToCurrencyId   = internalTransaction.ToCurrency!.Id,
                   ToAmount       = internalTransaction.ExchangeDetails!.ExchangeRate * internalTransaction.Amount,
                   FromBankAmount = internalTransaction.ExchangeDetails.ExchangeRate * internalTransaction.ExchangeDetails.AverageRate *
                                    internalTransaction.Amount,
                   IsDirect = false
               };
    }

    public static ProcessTransaction ToProcessTransaction(this PrepareExternalTransaction externalTransaction, Guid transactionId)
    {
        return new ProcessTransaction
               {
                   TransactionId  = transactionId,
                   FromAccountId  = externalTransaction.FromAccount!.Id,
                   FromCurrencyId = externalTransaction.FromCurrency!.Id,
                   FromAmount     = externalTransaction.Amount,
                   ToAccountId    = externalTransaction.ToAccount!.Id,
                   ToCurrencyId   = externalTransaction.ToCurrency!.Id,
                   ToAmount       = externalTransaction.ExchangeDetails!.ExchangeRate * externalTransaction.Amount,
                   FromBankAmount = externalTransaction.ExchangeDetails.ExchangeRate * externalTransaction.ExchangeDetails.AverageRate *
                                    externalTransaction.Amount,
                   IsDirect = false
               };
    }

    #endregion
}
