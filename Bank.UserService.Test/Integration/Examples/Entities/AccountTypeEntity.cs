using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class AccountType
        {
            public static readonly Guid Id = Seeder.AccountType.CheckingAccount.Id;
        }
    }
}
