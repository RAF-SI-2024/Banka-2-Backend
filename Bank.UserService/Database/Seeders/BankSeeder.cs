using System.Collections.Immutable;

using Configuration = Bank.UserService.Configurations.Configuration;

namespace Bank.UserService.Database.Seeders;

using BankModel = Models.Bank;

public static partial class Seeder
{
    public static class Bank
    {
        public static readonly BankModel Bank01 = new()
                                                  {
                                                      Id         = Guid.Parse("d5b232ec-03a6-4b78-9479-6300787c2a0b"),
                                                      Name       = "Bank 1",
                                                      Code       = "111",
                                                      BaseUrl    = Configuration.ExternalBank.Bank1BaseUrl,
                                                      CreatedAt  = DateTime.UtcNow,
                                                      ModifiedAt = DateTime.UtcNow
                                                  };

        public static readonly BankModel Bank02 = new()
                                                  {
                                                      Id         = Guid.Parse("330fc4a6-4213-4079-bd1f-438c05f8a3e8"),
                                                      Name       = "Bank 2",
                                                      Code       = "222",
                                                      BaseUrl    = "null",
                                                      CreatedAt  = DateTime.UtcNow,
                                                      ModifiedAt = DateTime.UtcNow
                                                  };

        public static readonly BankModel Bank03 = new()
                                                  {
                                                      Id         = Guid.Parse("f3f224ea-01c8-4a4b-80ea-8f4f492608be"),
                                                      Name       = "Bank 3",
                                                      Code       = "333",
                                                      BaseUrl    = Configuration.ExternalBank.Bank3BaseUrl,
                                                      CreatedAt  = DateTime.UtcNow,
                                                      ModifiedAt = DateTime.UtcNow
                                                  };

        public static readonly BankModel Bank04 = new()
                                                  {
                                                      Id         = Guid.Parse("c2fd28d3-6757-4993-9665-2b999b1c6cc2"),
                                                      Name       = "Bank 4",
                                                      Code       = "444",
                                                      BaseUrl    = Configuration.ExternalBank.Bank4BaseUrl,
                                                      CreatedAt  = DateTime.UtcNow,
                                                      ModifiedAt = DateTime.UtcNow
                                                  };

        public static readonly ImmutableArray<BankModel> All = [Bank01, Bank02, Bank03, Bank04];
    }
}
