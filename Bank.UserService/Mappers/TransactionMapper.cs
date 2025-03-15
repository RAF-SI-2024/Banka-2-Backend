using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;
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
                   Purpose         = transaction.Purpose,
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
                   Purpose         = transaction.Purpose,
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
                   FromAccountId   = transactionCreateRequest.FromAccountId,
                   FromCurrencyId  = transactionCreateRequest.FromCurrencyId,
                   ToAccountId     = null, // TODO: vrv null 
                   ToCurrencyId    = transactionCreateRequest.ToCurrencyId,
                   FromAmount      = transactionCreateRequest.Amount, // TODO: currency
                   ToAmount        = 0,                               // TODO: currency optional
                   CodeId          = transactionCreateRequest.CodeId,
                   Code            = code,
                   ReferenceNumber = transactionCreateRequest.ReferenceNumber,
                   Purpose         = transactionCreateRequest.Purpose,
                   Status          = TransactionStatus.Pending,
                   CreatedAt       = DateTime.UtcNow,
                   ModifiedAt      = DateTime.UtcNow,
               };
    }

    public static Transaction ToTransaction(this TransactionUpdateRequest transactionUpdateRequest, Transaction oldTransaction)
    {
        return new Transaction
               {
                   Id              = oldTransaction.Id,
                   FromAccountId   = oldTransaction.FromAccountId,
                   FromCurrencyId  = oldTransaction.FromCurrencyId,
                   ToAccountId     = oldTransaction.ToAccountId,
                   ToCurrencyId    = oldTransaction.ToCurrencyId,
                   FromAmount      = oldTransaction.FromAmount,
                   ToAmount        = oldTransaction.ToAmount,
                   Code            = oldTransaction.Code,
                   ReferenceNumber = oldTransaction.ReferenceNumber,
                   Purpose         = oldTransaction.Purpose,
                   Status          = transactionUpdateRequest.Status,
                   CreatedAt       = oldTransaction.CreatedAt,
                   CodeId          = oldTransaction.CodeId,
                   ModifiedAt      = DateTime.UtcNow,
               };
    }
}
