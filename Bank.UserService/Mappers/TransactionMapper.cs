using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class TransactionMapper
{
    public static TransactionCreateResponse ToCreateResponse(this Transaction transaction)
    {
        return new TransactionCreateResponse
               {
                   Id              = transaction.Id,
                   FromAmount      = transaction.FromAmount,
                   Code            = transaction.Code?.ToResponse()!,
                   ReferenceNumber = transaction.ReferenceNumber!,
                   Purpose         = transaction.Purpose ?? "",
                   Status          = transaction.Status,
                   CreatedAt       = transaction.CreatedAt,
                   ModifiedAt      = transaction.ModifiedAt,
               };
    }

    public static TransactionResponse ToResponse(this Transaction transaction)
    {
        return new TransactionResponse
               {
                   Id              = transaction.Id,
                   FromAccount     = transaction.FromAccount?.ToSimpleResponse()!,
                   ToAccount       = transaction.ToAccount?.ToSimpleResponse()!,
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

    public static Transaction ToTransaction(this TransactionCreateRequest transactionCreateRequest, TransactionCode code)
    {
        return new Transaction
               {
                   Id              = Guid.NewGuid(),
                   FromAccountId   = null,
                   FromCurrencyId  = transactionCreateRequest.FromCurrencyId,
                   ToAccountId     = null,
                   ToCurrencyId    = transactionCreateRequest.ToCurrencyId,
                   FromAmount      = transactionCreateRequest.Amount,
                   ToAmount        = 0,
                   CodeId          = transactionCreateRequest.CodeId,
                   ReferenceNumber = transactionCreateRequest.ReferenceNumber,
                   Purpose         = transactionCreateRequest.Purpose,
                   Status          = TransactionStatus.Pending,
                   CreatedAt       = DateTime.UtcNow,
                   ModifiedAt      = DateTime.UtcNow,
               };
    }

    public static Transaction ToTransaction(this Transaction transaction, TransactionUpdateRequest transactionUpdateRequest)
    {
        transaction.Status     = transactionUpdateRequest.Status;
        transaction.ModifiedAt = DateTime.UtcNow;
        return transaction;
    }

    public static Transaction ToTransaction(this PrepareWithdrawTransaction withdrawTransaction)
    {
        return new Transaction
               {
                   Id             = Guid.NewGuid(),
                   FromAccountId  = withdrawTransaction.Account.Id,
                   FromCurrencyId = withdrawTransaction.CurrencyId,
                   FromAmount     = withdrawTransaction.Amount,
                   CodeId         = Seeder.TransactionCode.TransactionCode266.Id,
                   Status         = TransactionStatus.Pending,
                   CreatedAt      = DateTime.UtcNow,
                   ModifiedAt     = DateTime.UtcNow
               };
    }

    public static Transaction ToTransaction(this PrepareDepositTransaction depositTransaction)
    {
        return new Transaction
               {
                   Id           = Guid.NewGuid(),
                   ToAccountId  = depositTransaction.Account!.Id,
                   ToCurrencyId = depositTransaction.CurrencyId,
                   ToAmount     = depositTransaction.Amount,
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
                   FromCurrencyId  = internalTransaction.FromCurrencyId,
                   FromAmount      = internalTransaction.Amount,
                   ToAccountId     = internalTransaction.ToAccount!.Id,
                   ToCurrencyId    = internalTransaction.ToCurrencyId,
                   ToAmount        = internalTransaction.Amount,
                   CodeId          = internalTransaction.TransactionCodeId,
                   Status          = TransactionStatus.Pending,
                   Purpose         = internalTransaction.Purpose,
                   ReferenceNumber = internalTransaction.ReferenceNumber,
                   CreatedAt       = DateTime.UtcNow,
                   ModifiedAt      = DateTime.UtcNow
               };
    }

    public static ProcessTransaction ToProcessTransaction(this PrepareWithdrawTransaction withdrawTransaction, Guid transactionId)
    {
        return new ProcessTransaction
               {
                   TransactionId  = transactionId,
                   FromAccountId  = withdrawTransaction.Account.Id,
                   FromCurrencyId = withdrawTransaction.CurrencyId,
                   FromAmount     = withdrawTransaction.Amount,
                   ToAccountId    = Guid.Empty,
                   ToCurrencyId   = Guid.Empty,
                   ToAmount       = 0,
                   FromBankAmount = withdrawTransaction.Amount
               };
    }

    public static ProcessTransaction ToProcessTransaction(this PrepareDepositTransaction depositTransaction, Guid transactionId)
    {
        return new ProcessTransaction
               {
                   TransactionId  = transactionId,
                   FromAccountId  = Guid.Empty,
                   FromCurrencyId = Guid.Empty,
                   FromAmount     = 0,
                   ToAccountId    = depositTransaction.Account.Id,
                   ToCurrencyId   = depositTransaction.CurrencyId,
                   ToAmount       = depositTransaction.Amount,
                   FromBankAmount = 0
               };
    }

    public static ProcessTransaction ToProcessTransaction(this PrepareInternalTransaction internalTransaction, Guid transactionId)
    {
        return new ProcessTransaction
               {
                   TransactionId  = transactionId,
                   FromAccountId  = internalTransaction.FromAccount!.Id,
                   FromCurrencyId = internalTransaction.FromCurrencyId,
                   FromAmount     = internalTransaction.Amount,
                   ToAccountId    = internalTransaction.ToAccount!.Id,
                   ToCurrencyId   = internalTransaction.ToCurrencyId,
                   ToAmount       = internalTransaction.ExchangeDetails.ExchangeRate * internalTransaction.Amount,
                   FromBankAmount = internalTransaction.ExchangeDetails.ExchangeRate * internalTransaction.ExchangeDetails.AverageRate *
                                    internalTransaction.Amount,
               };
    }
}
