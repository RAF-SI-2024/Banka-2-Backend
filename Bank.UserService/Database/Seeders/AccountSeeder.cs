using System.Collections.Immutable;

using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using AccountModel = Account;

public static partial class Seeder
{
    public static class Account
    {
        public static readonly AccountModel CountryAccount = new()
                                                             {
                                                                 Id               = Guid.Parse("89110ca6-41f9-4b6d-b302-45a87b401c02"),
                                                                 ClientId         = Client.Bank.Id,
                                                                 Name             = "Country",
                                                                 Number           = "999999999",
                                                                 Office           = "0000",
                                                                 Balance          = 1_000_000_000,
                                                                 AvailableBalance = 1_000_000_000,
                                                                 EmployeeId       = Employee.Admin.Id,
                                                                 CurrencyId       = Currency.SerbianDinar.Id,
                                                                 AccountTypeId    = AccountType.CheckingAccount.Id,
                                                                 DailyLimit       = 0m,
                                                                 MonthlyLimit     = 0m,
                                                                 CreationDate     = DateOnly.FromDateTime(DateTime.UtcNow),
                                                                 ExpirationDate   = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(999)),
                                                                 CreatedAt        = DateTime.UtcNow,
                                                                 ModifiedAt       = DateTime.UtcNow,
                                                                 Status           = true,
                                                             };

        public static readonly AccountModel BankAccount = new()
                                                          {
                                                              Id               = Guid.Parse("7763b3b9-98fa-4425-b588-289467965803"),
                                                              ClientId         = Client.Bank.Id,
                                                              Name             = "",
                                                              Number           = "000000000",
                                                              Office           = "0000",
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
                                                                    Office           = "0000",
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
                                                                    Office           = "0000",
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
                                                                   Office           = "0000",
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

        // MASSIVELY INCREASED: For BusinessLoan1 (USD account)
        public static readonly AccountModel ForeignAccount02 = new()
                                                               {
                                                                   Id               = Guid.Parse("1befdb51-989a-4e1f-b7bd-333cf29421b3"),
                                                                   ClientId         = Client.Client01.Id,
                                                                   Name             = "",
                                                                   Number           = "000000004",
                                                                   Office           = "0000",
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
                                                                   Office           = "0000",
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

        public static readonly AccountModel TradingAccount01 = new()
                                                               {
                                                                   Id               = Guid.Parse("e4df2e9b-a57f-460e-a79e-c6b1e47ef4ab"),
                                                                   ClientId         = Employee.Supervisor01.Id,
                                                                   Name             = "Supervisor Account",
                                                                   Number           = "100000000",
                                                                   Office           = "0000",
                                                                   Balance          = 100_000,
                                                                   AvailableBalance = 100_000,
                                                                   EmployeeId       = Employee.Admin.Id,
                                                                   CurrencyId       = Currency.Euro.Id,
                                                                   AccountTypeId    = AccountType.ForeignCurrencyAccount.Id,
                                                                   DailyLimit       = 9999999999999999.999999999999m,
                                                                   MonthlyLimit     = 9999999999999999.999999999999m,
                                                                   CreationDate     = DateOnly.FromDateTime(DateTime.UtcNow),
                                                                   ExpirationDate   = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(10)),
                                                                   CreatedAt        = DateTime.UtcNow,
                                                                   ModifiedAt       = DateTime.UtcNow,
                                                                   Status           = true
                                                               };

        public static readonly AccountModel TradingAccount02 = new()
                                                               {
                                                                   Id               = Guid.Parse("633419a2-21d5-420c-a951-a4a1b9b351c0"),
                                                                   ClientId         = Employee.Agent01.Id,
                                                                   Name             = "",
                                                                   Number           = "100000001",
                                                                   Office           = "0000",
                                                                   Balance          = 10_000,
                                                                   AvailableBalance = 10_000,
                                                                   EmployeeId       = Employee.Supervisor01.Id,
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

        public static readonly ImmutableArray<AccountModel> All =
        [
            CountryAccount, BankAccount, DomesticAccount01, DomesticAccount02, ForeignAccount01, ForeignAccount02,
            ForeignAccount03, TradingAccount01, TradingAccount02
        ];
    }
}
