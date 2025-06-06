using Bank.UserService.Database.Seeders;
using Bank.UserService.Repositories;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Email
        {
            public static readonly EmailType EmailType = EmailType.LoanInstallmentPaid;

            public static readonly Models.Client Client = Seeder.Client.Client01;

            public static readonly decimal InstallmentAmount = 2000m;

            public static readonly string Code = "EUR";

            public static readonly decimal RemainingAmount = 10000m;
        }
    }
}
