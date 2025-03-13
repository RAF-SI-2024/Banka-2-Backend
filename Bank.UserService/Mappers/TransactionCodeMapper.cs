using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class TransactionCodeMapper
{
    public static TransactionCodeResponse ToResponse(this TransactionCode transactionCode)
    {
        return new TransactionCodeResponse
               {
                   Id         = transactionCode.Id,
                   Code       = transactionCode.Code,
                   Name       = transactionCode.Name,
                   CreatedAt  = DateTime.UtcNow,
                   ModifiedAt = DateTime.UtcNow,
               };
    }
}
