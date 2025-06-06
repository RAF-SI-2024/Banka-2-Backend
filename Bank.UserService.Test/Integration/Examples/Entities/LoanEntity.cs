using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Loan
        {
            public static readonly LoanCreateRequest Request = Database.Examples.Example.Loan.CreateRequest;

            public static readonly LoanUpdateRequest UpdateRequest = Database.Examples.Example.Loan.UpdateRequest;

            public static readonly Guid Id = Seeder.Loan.PersonalLoan1.Id;

            public static readonly Guid ClientId = Seeder.Client.Client01.Id;
        }
    }
}
