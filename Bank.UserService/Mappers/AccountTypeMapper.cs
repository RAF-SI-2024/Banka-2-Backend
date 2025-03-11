using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class AccountTypeMapper
{
    public static AccountTypeResponse ToResponse(this AccountType accountType)
    {
        return new AccountTypeResponse
               {
                   Id         = accountType.Id,
                   Name       = accountType.Name,
                   Code       = accountType.Code,
                   CreatedAt  = accountType.CreatedAt,
                   ModifiedAt = accountType.ModifiedAt
               };
    }
}
