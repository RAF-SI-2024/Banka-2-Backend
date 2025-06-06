﻿using System.Collections.Immutable;

using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using AccountCurrencyModel = AccountCurrency;

public static partial class Seeder
{
    public static class AccountCurrency
    {
        public static readonly AccountCurrencyModel BankAustralianDollar = new()
                                                                           {
                                                                               Id               = Guid.Parse("cef7936b-73c0-4ae5-a0e0-000a4bbac749"),
                                                                               Balance          = 1_500_000,
                                                                               AvailableBalance = 1_500_000,
                                                                               AccountId        = Account.BankAccount.Id,
                                                                               EmployeeId       = Employee.Admin.Id,
                                                                               CurrencyId       = Currency.AustralianDollar.Id,
                                                                               DailyLimit       = 9999999999999999.999999999999m,
                                                                               MonthlyLimit     = 9999999999999999.999999999999m,
                                                                               CreatedAt        = DateTime.UtcNow,
                                                                               ModifiedAt       = DateTime.UtcNow,
                                                                           };

        public static readonly AccountCurrencyModel BankBritishPound = new()
                                                                       {
                                                                           Id               = Guid.Parse("941f9267-0a44-43c4-8de8-0c53489358df"),
                                                                           Balance          = 5_000_000,
                                                                           AvailableBalance = 5_000_000,
                                                                           AccountId        = Account.BankAccount.Id,
                                                                           EmployeeId       = Employee.Admin.Id,
                                                                           CurrencyId       = Currency.BritishPound.Id,
                                                                           DailyLimit       = 9999999999999999.999999999999m,
                                                                           MonthlyLimit     = 9999999999999999.999999999999m,
                                                                           CreatedAt        = DateTime.UtcNow,
                                                                           ModifiedAt       = DateTime.UtcNow,
                                                                       };

        public static readonly AccountCurrencyModel BankCanadianDollar = new()
                                                                         {
                                                                             Id               = Guid.Parse("baa7f2af-71d1-41bb-9d31-018ac3ece675"),
                                                                             Balance          = 1_000_000,
                                                                             AvailableBalance = 1_000_000,
                                                                             AccountId        = Account.BankAccount.Id,
                                                                             EmployeeId       = Employee.Admin.Id,
                                                                             CurrencyId       = Currency.CanadianDollar.Id,
                                                                             DailyLimit       = 9999999999999999.999999999999m,
                                                                             MonthlyLimit     = 9999999999999999.999999999999m,
                                                                             CreatedAt        = DateTime.UtcNow,
                                                                             ModifiedAt       = DateTime.UtcNow,
                                                                         };

        public static readonly AccountCurrencyModel BankSwissFranc = new()
                                                                     {
                                                                         Id               = Guid.Parse("4e897ee0-9374-4fc4-a738-0127d89d5088"),
                                                                         Balance          = 5_000_000,
                                                                         AvailableBalance = 5_000_000,
                                                                         AccountId        = Account.BankAccount.Id,
                                                                         EmployeeId       = Employee.Admin.Id,
                                                                         CurrencyId       = Currency.SwissFranc.Id,
                                                                         DailyLimit       = 9999999999999999.999999999999m,
                                                                         MonthlyLimit     = 9999999999999999.999999999999m,
                                                                         CreatedAt        = DateTime.UtcNow,
                                                                         ModifiedAt       = DateTime.UtcNow,
                                                                     };

        public static readonly AccountCurrencyModel BankEuro = new()
                                                               {
                                                                   Id               = Guid.Parse("82e7f0c9-732b-4d93-8f33-95f26ef41ad6"),
                                                                   Balance          = 10_000_000,
                                                                   AvailableBalance = 10_000_000,
                                                                   AccountId        = Account.BankAccount.Id,
                                                                   EmployeeId       = Employee.Admin.Id,
                                                                   CurrencyId       = Currency.Euro.Id,
                                                                   DailyLimit       = 9999999999999999.999999999999m,
                                                                   MonthlyLimit     = 9999999999999999.999999999999m,
                                                                   CreatedAt        = DateTime.UtcNow,
                                                                   ModifiedAt       = DateTime.UtcNow,
                                                               };

        public static readonly AccountCurrencyModel BankJapaneseYen = new()
                                                                      {
                                                                          Id               = Guid.Parse("29c3ed2f-1042-4ea2-870e-490588d03bf3"),
                                                                          Balance          = 100_000_000,
                                                                          AvailableBalance = 100_000_000,
                                                                          AccountId        = Account.BankAccount.Id,
                                                                          EmployeeId       = Employee.Admin.Id,
                                                                          CurrencyId       = Currency.JapaneseYen.Id,
                                                                          DailyLimit       = 9999999999999999.999999999999m,
                                                                          MonthlyLimit     = 9999999999999999.999999999999m,
                                                                          CreatedAt        = DateTime.UtcNow,
                                                                          ModifiedAt       = DateTime.UtcNow,
                                                                      };

        public static readonly AccountCurrencyModel BankUSDollar = new()
                                                                   {
                                                                       Id               = Guid.Parse("796dc52b-7c3b-4aa3-a189-ca3c433f6724"),
                                                                       Balance          = 10_000_000,
                                                                       AvailableBalance = 10_000_000,
                                                                       AccountId        = Account.BankAccount.Id,
                                                                       EmployeeId       = Employee.Admin.Id,
                                                                       CurrencyId       = Currency.USDollar.Id,
                                                                       DailyLimit       = 9999999999999999.999999999999m,
                                                                       MonthlyLimit     = 9999999999999999.999999999999m,
                                                                       CreatedAt        = DateTime.UtcNow,
                                                                       ModifiedAt       = DateTime.UtcNow,
                                                                   };

        public static readonly AccountCurrencyModel ForeignAccount01USDollar = new()
                                                                               {
                                                                                   Id               = Guid.Parse("98dbae1e-ba52-4b84-9fea-0f4b2ef4320f"),
                                                                                   Balance          = 500,
                                                                                   AvailableBalance = 500,
                                                                                   AccountId        = Account.ForeignAccount01.Id,
                                                                                   EmployeeId       = Employee.Employee01.Id,
                                                                                   CurrencyId       = Currency.USDollar.Id,
                                                                                   DailyLimit       = 1000,
                                                                                   MonthlyLimit     = 1000,
                                                                                   CreatedAt        = DateTime.UtcNow,
                                                                                   ModifiedAt       = DateTime.UtcNow,
                                                                               };

        public static readonly AccountCurrencyModel ForeignAccount01BritishPound = new()
                                                                                   {
                                                                                       Id               = Guid.Parse("107aa7e2-f074-42bc-bbd1-08bcacea7c42"),
                                                                                       Balance          = 200,
                                                                                       AvailableBalance = 200,
                                                                                       AccountId        = Account.ForeignAccount01.Id,
                                                                                       EmployeeId       = Employee.Employee01.Id,
                                                                                       CurrencyId       = Currency.BritishPound.Id,
                                                                                       DailyLimit       = 1000,
                                                                                       MonthlyLimit     = 1000,
                                                                                       CreatedAt        = DateTime.UtcNow,
                                                                                       ModifiedAt       = DateTime.UtcNow,
                                                                                   };

        public static readonly AccountCurrencyModel ForeignAccount01CanadianDollar = new()
                                                                                     {
                                                                                         Id               = Guid.Parse("cae02aaa-06e5-4196-9cc9-03c11cee1516"),
                                                                                         Balance          = 100,
                                                                                         AvailableBalance = 100,
                                                                                         AccountId        = Account.ForeignAccount01.Id,
                                                                                         EmployeeId       = Employee.Employee03.Id,
                                                                                         CurrencyId       = Currency.CanadianDollar.Id,
                                                                                         DailyLimit       = 1000,
                                                                                         MonthlyLimit     = 1000,
                                                                                         CreatedAt        = DateTime.UtcNow,
                                                                                         ModifiedAt       = DateTime.UtcNow,
                                                                                     };

        public static readonly AccountCurrencyModel ForeignAccount02CanadianDollar = new()
                                                                                     {
                                                                                         Id               = Guid.Parse("5b17f090-c895-47c5-af90-4c72892fdc40"),
                                                                                         Balance          = 900,
                                                                                         AvailableBalance = 900,
                                                                                         AccountId        = Account.ForeignAccount02.Id,
                                                                                         EmployeeId       = Employee.Employee02.Id,
                                                                                         CurrencyId       = Currency.CanadianDollar.Id,
                                                                                         DailyLimit       = 1000,
                                                                                         MonthlyLimit     = 1000,
                                                                                         CreatedAt        = DateTime.UtcNow,
                                                                                         ModifiedAt       = DateTime.UtcNow,
                                                                                     };

        public static readonly ImmutableArray<AccountCurrencyModel> All =
        [
            BankAustralianDollar, BankBritishPound, BankCanadianDollar, BankSwissFranc, BankEuro, BankJapaneseYen,
            BankUSDollar, ForeignAccount01USDollar, ForeignAccount01BritishPound, ForeignAccount01CanadianDollar,
            ForeignAccount02CanadianDollar
        ];
    }
}
