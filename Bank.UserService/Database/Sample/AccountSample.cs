using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
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
    }
}
