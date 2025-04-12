using Bank.Application.Responses;

using BankModel = Bank.UserService.Models.Bank;

namespace Bank.UserService.Mappers;

public static class BankMapper
{
    public static BankResponse ToResponse(this BankModel bank)
    {
        return new BankResponse
               {
                   Id         = bank.Id,
                   Name       = bank.Name,
                   Code       = bank.Code,
                   BaseUrl    = bank.BaseUrl,
                   CreatedAt  = bank.CreatedAt,
                   ModifiedAt = bank.ModifiedAt
               };
    }
}
