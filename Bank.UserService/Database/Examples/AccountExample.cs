using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Examples;

file static class Values
{
    public static readonly Guid   Id            = Guid.Parse("3f4b1e6e-a2f5-4e3b-8f88-2f70a6b42b19");
    public const           string AccountNumber = "222001112345678922";
}

public static partial class Example
{
    public static class Account
    {
        public static readonly AccountCreateRequest CreateRequest = new()
                                                                    {
                                                                        Name          = "Štedni račun",
                                                                        DailyLimit    = 2000.00m,
                                                                        MonthlyLimit  = 50000.00m,
                                                                        ClientId      = Seeder.Client.Client02.Id,
                                                                        Balance       = 5000.00m,
                                                                        CurrencyId    = Seeder.Currency.SerbianDinar.Id,
                                                                        AccountTypeId = Seeder.AccountType.SavingsAccount.Id,
                                                                        Status        = true
                                                                    };

        public static readonly AccountUpdateClientRequest UpdateClientRequest = new()
                                                                                {
                                                                                    Name         = "Štedni račun",
                                                                                    DailyLimit   = 2500.00m,
                                                                                    MonthlyLimit = 60000.00m,
                                                                                };

        public static readonly AccountUpdateEmployeeRequest UpdateEmployeeRequest = new()
                                                                                    {
                                                                                        Status = true
                                                                                    };

        public static readonly AccountResponse Response = new()
                                                          {
                                                              Id                = Values.Id,
                                                              AccountNumber     = Values.AccountNumber,
                                                              Office            = "0000",
                                                              Name              = CreateRequest.Name,
                                                              Balance           = CreateRequest.Balance,
                                                              AvailableBalance  = 4500.50m,
                                                              Type              = null!,
                                                              Currency          = null!,
                                                              Employee          = null!,
                                                              Client            = null!,
                                                              AccountCurrencies = [],
                                                              DailyLimit        = CreateRequest.DailyLimit,
                                                              MonthlyLimit      = CreateRequest.MonthlyLimit,
                                                              CreationDate      = new(2023, 5, 15),
                                                              ExpirationDate    = new(2033, 5, 15),
                                                              Status            = true,
                                                              CreatedAt         = DateTime.UtcNow,
                                                              ModifiedAt        = DateTime.UtcNow,
                                                          };

        public static readonly AccountSimpleResponse SimpleResponse = new()
                                                                      {
                                                                          Id            = Values.Id,
                                                                          AccountNumber = Values.AccountNumber
                                                                      };
    }
}
