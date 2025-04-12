using Bank.UserService.Mappers;
using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using AccountModel = Account;

public static partial class Seeder
{
    public static class Account
    {
        public static readonly AccountModel BankAccount = new()
                                                          {
                                                              Id               = Guid.Parse("7763b3b9-98fa-4425-b588-289467965803"),
                                                              ClientId         = Client.Bank.Id,
                                                              Name             = "",
                                                              Number           = "000000000",
                                                              Balance          = 1_000_000_000,
                                                              AvailableBalance = 1_000_000_000,
                                                              EmployeeId       = Employee.Admin.Id,
                                                              CurrencyId       = Currency.SerbianDinar.Id,
                                                              AccountTypeId    = AccountType.BusinessForeignCurrencyAccount.Id,
                                                              DailyLimit       = 9999999999999999.999999999999m,
                                                              MonthlyLimit     = 9999999999999999.999999999999m,
                                                              CreationDate     = DateOnly.FromDateTime(DateTime.UtcNow),
                                                              ExpirationDate   = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(999)),
                                                              CreatedAt        = DateTime.UtcNow,
                                                              ModifiedAt       = DateTime.UtcNow,
                                                              Status           = true
                                                          };

        public static readonly AccountModel DomesticAccount01 = new()
                                                                {
                                                                    Id               = Guid.Parse("5d5fa996-9533-421c-a319-cd43ff41d86f"),
                                                                    ClientId         = Client.Client01.Id,
                                                                    Name             = "",
                                                                    Number           = "000000001",
                                                                    Balance          = 10_000,
                                                                    AvailableBalance = 10_000,
                                                                    EmployeeId       = Employee.Employee01.Id,
                                                                    CurrencyId       = Currency.SerbianDinar.Id,
                                                                    AccountTypeId    = AccountType.CheckingAccount.Id,
                                                                    DailyLimit       = 100_000,
                                                                    MonthlyLimit     = 100_000,
                                                                    CreationDate     = DateOnly.FromDateTime(DateTime.UtcNow),
                                                                    ExpirationDate   = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(10)),
                                                                    CreatedAt        = DateTime.UtcNow,
                                                                    ModifiedAt       = DateTime.UtcNow,
                                                                    Status           = true
                                                                };

        public static readonly AccountModel DomesticAccount02 = new()
                                                                {
                                                                    Id               = Guid.Parse("b5f4b482-3e93-482f-9687-4d58c473fc4d"),
                                                                    ClientId         = Client.Client01.Id,
                                                                    Name             = "",
                                                                    Number           = "000000002",
                                                                    Balance          = 200_000,
                                                                    AvailableBalance = 200_000,
                                                                    EmployeeId       = Employee.Employee01.Id,
                                                                    CurrencyId       = Currency.SerbianDinar.Id,
                                                                    AccountTypeId    = AccountType.CheckingAccount.Id,
                                                                    DailyLimit       = 100_000,
                                                                    MonthlyLimit     = 100_000,
                                                                    CreationDate     = DateOnly.FromDateTime(DateTime.UtcNow),
                                                                    ExpirationDate   = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(10)),
                                                                    CreatedAt        = DateTime.UtcNow,
                                                                    ModifiedAt       = DateTime.UtcNow,
                                                                    Status           = true
                                                                };

        public static readonly AccountModel ForeignAccount01 = new()
                                                               {
                                                                   Id               = Guid.Parse("6a376abc-eaa3-4438-b159-c06f29d04e68"),
                                                                   ClientId         = Client.Client01.Id,
                                                                   Name             = "",
                                                                   Number           = "000000003",
                                                                   Balance          = 3031,
                                                                   AvailableBalance = 3031,
                                                                   EmployeeId       = Employee.Employee01.Id,
                                                                   CurrencyId       = Currency.Euro.Id,
                                                                   AccountTypeId    = AccountType.ForeignCurrencyAccount.Id,
                                                                   DailyLimit       = 1000,
                                                                   MonthlyLimit     = 1000,
                                                                   CreationDate     = DateOnly.FromDateTime(DateTime.UtcNow),
                                                                   ExpirationDate   = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(10)),
                                                                   CreatedAt        = DateTime.UtcNow,
                                                                   ModifiedAt       = DateTime.UtcNow,
                                                                   Status           = true
                                                               };

        public static readonly AccountModel ForeignAccount02 = new()
                                                               {
                                                                   Id               = Guid.Parse("1befdb51-989a-4e1f-b7bd-333cf29421b3"),
                                                                   ClientId         = Client.Client01.Id,
                                                                   Name             = "",
                                                                   Number           = "000000004",
                                                                   Balance          = 1000,
                                                                   AvailableBalance = 1000,
                                                                   EmployeeId       = Employee.Employee01.Id,
                                                                   CurrencyId       = Currency.USDollar.Id,
                                                                   AccountTypeId    = AccountType.BusinessForeignCurrencyAccount.Id,
                                                                   DailyLimit       = 1000,
                                                                   MonthlyLimit     = 1000,
                                                                   CreationDate     = DateOnly.FromDateTime(DateTime.UtcNow),
                                                                   ExpirationDate   = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(10)),
                                                                   CreatedAt        = DateTime.UtcNow,
                                                                   ModifiedAt       = DateTime.UtcNow,
                                                                   Status           = true
                                                               };

        public static readonly AccountModel ForeignAccount03 = new()
                                                               {
                                                                   Id               = Guid.Parse("fdbc0d89-c9ee-4c6a-bf67-056039bc4c5b"),
                                                                   ClientId         = Client.Client01.Id,
                                                                   Name             = "",
                                                                   Number           = "000000005",
                                                                   Balance          = 1000,
                                                                   AvailableBalance = 1000,
                                                                   EmployeeId       = Employee.Employee01.Id,
                                                                   CurrencyId       = Currency.Euro.Id,
                                                                   AccountTypeId    = AccountType.ForeignCurrencyAccount.Id,
                                                                   DailyLimit       = 1000,
                                                                   MonthlyLimit     = 1000,
                                                                   CreationDate     = DateOnly.FromDateTime(DateTime.UtcNow),
                                                                   ExpirationDate   = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(10)),
                                                                   CreatedAt        = DateTime.UtcNow,
                                                                   ModifiedAt       = DateTime.UtcNow,
                                                                   Status           = true
                                                               };
    }
}

public static class AccountSeederExtension
{
    public static async Task SeedAccount(this ApplicationContext context)
    {
        if (context.Accounts.Any())
            return;

        var x = Seeder.Account.BankAccount;

        await context.Accounts.AddRangeAsync([
                                                 Seeder.Account.BankAccount, Seeder.Account.DomesticAccount01, Seeder.Account.DomesticAccount02, Seeder.Account.ForeignAccount01,
                                                 Seeder.Account.ForeignAccount02, Seeder.Account.ForeignAccount03
                                             ]);

        await context.SaveChangesAsync();
    }
}
