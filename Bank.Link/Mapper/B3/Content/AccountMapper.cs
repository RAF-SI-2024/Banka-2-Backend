using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.Link.Responses;

namespace Bank.Link.Mapper.B3.Content;

internal static class AccountMapper
{
    internal static AccountResponse ToNative(this Response.B3.AccountResponse response, CurrencyResponse currencyResponse, AccountTypeResponse accountTypeResponse)
    {
        return new AccountResponse
               {
                   Id                = Guid.Empty,
                   AccountNumber     = response.AccountNumber,
                   Office            = response.AccountNumber[3..7],
                   Name              = response.Name,
                   Client            = response.Owner.ToSimpleNative(),
                   Balance           = response.Balance,
                   AvailableBalance  = response.AvailableBalance,
                   Employee          = null!,
                   Currency          = currencyResponse,
                   Type              = accountTypeResponse,
                   AccountCurrencies = [],
                   DailyLimit        = response.DailyLimit,
                   MonthlyLimit      = response.MonthlyLimit,
                   CreationDate      = default,
                   ExpirationDate    = default,
                   Status            = false,
                   CreatedAt         = DateTime.UtcNow,
                   ModifiedAt        = DateTime.UtcNow
               };
    }

    internal static ClientResponse ToNative(this Response.B3.AccountClientResponse response)
    {
        return new ClientResponse
               {
                   Id                         = Guid.Empty,
                   FirstName                  = response.FirstName,
                   LastName                   = response.LastName,
                   DateOfBirth                = default,
                   Gender                     = Gender.Invalid,
                   UniqueIdentificationNumber = "0000000000000",
                   Email                      = response.Email,
                   PhoneNumber                = "",
                   Address                    = "",
                   Accounts                   = [],
                   Role                       = Role.Invalid,
                   Permissions                = 0,
                   CreatedAt                  = DateTime.UtcNow,
                   ModifiedAt                 = DateTime.UtcNow,
                   Activated                  = false
               };
    }

    internal static ClientSimpleResponse ToSimpleNative(this Response.B3.AccountClientResponse response)
    {
        return new ClientSimpleResponse
               {
                   Id                         = Guid.Empty,
                   FirstName                  = response.FirstName,
                   LastName                   = response.LastName,
                   DateOfBirth                = default,
                   Gender                     = Gender.Invalid,
                   UniqueIdentificationNumber = "0000000000000",
                   Email                      = response.Email,
                   PhoneNumber                = "",
                   Address                    = "",
                   Role                       = Role.Invalid,
                   Permissions                = 0,
                   CreatedAt                  = DateTime.UtcNow,
                   ModifiedAt                 = DateTime.UtcNow,
                   Activated                  = false
               };
    }
}
