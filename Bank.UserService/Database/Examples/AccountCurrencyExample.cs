using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Examples;

public static partial class Example
{
    public static class AccountCurrency
    {
        public static readonly AccountCurrencyCreateRequest CreateRequest = new()
                                                                            {
                                                                                EmployeeId   = Seeder.Employee.Employee01.Id,
                                                                                CurrencyId   = Seeder.Currency.SerbianDinar.Id,
                                                                                AccountId    = Seeder.Account.DomesticAccount01.Id,
                                                                                DailyLimit   = 1000,
                                                                                MonthlyLimit = 3000
                                                                            };

        public static readonly AccountCurrencyClientUpdateRequest ClientUpdateRequest = new()
                                                                                        {
                                                                                            DailyLimit   = 2000,
                                                                                            MonthlyLimit = 4000
                                                                                        };

        public static readonly AccountCurrencyResponse Response = new()
                                                                  {
                                                                      Id               = Guid.Parse("d4e5f6a7-b8c9-40d1-a2b3-c4d5e6f78901"),
                                                                      Account          = null!,
                                                                      Employee         = null!,
                                                                      Currency         = null!,
                                                                      Balance          = 12000.75m,
                                                                      AvailableBalance = 8000.50m,
                                                                      DailyLimit       = CreateRequest.DailyLimit,
                                                                      MonthlyLimit     = CreateRequest.MonthlyLimit,
                                                                      CreatedAt        = DateTime.UtcNow,
                                                                      ModifiedAt       = DateTime.UtcNow
                                                                  };
    }
}
