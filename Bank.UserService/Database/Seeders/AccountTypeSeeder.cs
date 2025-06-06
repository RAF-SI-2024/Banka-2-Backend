﻿using System.Collections.Immutable;

using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using AccountTypeModel = AccountType;

public static partial class Seeder
{
    public static class AccountType
    {
        public static readonly AccountTypeModel CheckingAccount = new()
                                                                  {
                                                                      Id         = Guid.Parse("f606cd71-f42f-4ca4-a532-5254bfe34920"),
                                                                      Name       = "Personal Checking Account",
                                                                      Code       = "11",
                                                                      CreatedAt  = DateTime.UtcNow,
                                                                      ModifiedAt = DateTime.UtcNow,
                                                                  };

        public static readonly AccountTypeModel SavingsAccount = new()
                                                                 {
                                                                     Id         = Guid.Parse("316c6bd3-f8ee-4db3-a21c-ca68c605c3e2"),
                                                                     Name       = "Savings Account",
                                                                     Code       = "12",
                                                                     CreatedAt  = DateTime.UtcNow,
                                                                     ModifiedAt = DateTime.UtcNow,
                                                                 };

        public static readonly AccountTypeModel PensionAccount = new()
                                                                 {
                                                                     Id         = Guid.Parse("a86408e7-8adf-4bb2-89d4-042302ec1121"),
                                                                     Name       = "Pension Account",
                                                                     Code       = "13",
                                                                     CreatedAt  = DateTime.UtcNow,
                                                                     ModifiedAt = DateTime.UtcNow,
                                                                 };

        public static readonly AccountTypeModel YouthAccount = new()
                                                               {
                                                                   Id         = Guid.Parse("17ef6c0a-5f51-4d81-9e7f-b0891549ad31"),
                                                                   Name       = "Youth Account",
                                                                   Code       = "14",
                                                                   CreatedAt  = DateTime.UtcNow,
                                                                   ModifiedAt = DateTime.UtcNow,
                                                               };

        public static readonly AccountTypeModel StudentAccount = new()
                                                                 {
                                                                     Id         = Guid.Parse("785a5564-ede2-4ea6-bff8-43cff8e3bfea"),
                                                                     Name       = "Student Account",
                                                                     Code       = "15",
                                                                     CreatedAt  = DateTime.UtcNow,
                                                                     ModifiedAt = DateTime.UtcNow,
                                                                 };

        public static readonly AccountTypeModel UnemploymentAccount = new()
                                                                      {
                                                                          Id         = Guid.Parse("3bb76b0c-01c3-4653-a109-877aef0025fe"),
                                                                          Name       = "Unemployment Account",
                                                                          Code       = "16",
                                                                          CreatedAt  = DateTime.UtcNow,
                                                                          ModifiedAt = DateTime.UtcNow,
                                                                      };

        public static readonly AccountTypeModel BusinessLLCAccount = new()
                                                                     {
                                                                         Id         = Guid.Parse("3b9178e0-44e2-42d3-9715-52bd9a3267a1"),
                                                                         Name       = "LLC",
                                                                         Code       = "21",
                                                                         CreatedAt  = DateTime.UtcNow,
                                                                         ModifiedAt = DateTime.UtcNow,
                                                                     };

        public static readonly AccountTypeModel BusinessJointStockCompanyAccount = new()
                                                                                   {
                                                                                       Id         = Guid.Parse("69a5c811-e024-4d73-863a-7c7e4eabcff5"),
                                                                                       Name       = "Joint-Stock Company",
                                                                                       Code       = "22",
                                                                                       CreatedAt  = DateTime.UtcNow,
                                                                                       ModifiedAt = DateTime.UtcNow,
                                                                                   };

        public static readonly AccountTypeModel BusinessFoundationAccount = new()
                                                                            {
                                                                                Id         = Guid.Parse("3aa7ad0a-db84-41a9-be59-962e430c6c4c"),
                                                                                Name       = "Foundation",
                                                                                Code       = "23",
                                                                                CreatedAt  = DateTime.UtcNow,
                                                                                ModifiedAt = DateTime.UtcNow,
                                                                            };

        public static readonly AccountTypeModel ForeignCurrencyAccount = new()
                                                                         {
                                                                             Id         = Guid.Parse("4115d886-5851-4d8c-ad7d-d18c3d90b77a"),
                                                                             Name       = "Foreign Currency Account",
                                                                             Code       = "31",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow,
                                                                         };

        public static readonly AccountTypeModel BusinessForeignCurrencyAccount = new()
                                                                                 {
                                                                                     Id         = Guid.Parse("81fc131d-0098-48ce-814e-1570ed5723d5"),
                                                                                     Name       = "Business Foreign Currency Account",
                                                                                     Code       = "41",
                                                                                     CreatedAt  = DateTime.UtcNow,
                                                                                     ModifiedAt = DateTime.UtcNow,
                                                                                 };

        public static readonly ImmutableArray<AccountTypeModel> All =
        [
            CheckingAccount, SavingsAccount, PensionAccount, YouthAccount, StudentAccount, UnemploymentAccount,
            BusinessLLCAccount, BusinessJointStockCompanyAccount, BusinessFoundationAccount, ForeignCurrencyAccount,
            BusinessForeignCurrencyAccount,
        ];
    }
}
