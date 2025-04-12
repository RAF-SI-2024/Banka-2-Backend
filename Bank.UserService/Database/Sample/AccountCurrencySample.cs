using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
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

        public static readonly AccountCurrencyClientUpdateRequest UpdateRequest = new()
                                                                                  {
                                                                                      DailyLimit   = 2000,
                                                                                      MonthlyLimit = 4000
                                                                                  };
    }
}
