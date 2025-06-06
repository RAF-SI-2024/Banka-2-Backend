using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Bank
        {
            public static readonly Guid BankId = Seeder.Bank.Bank02.Id;
        }
    }
}
