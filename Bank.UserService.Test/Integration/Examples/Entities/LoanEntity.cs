using Bank.Application.Requests;
using Bank.UserService.Database.Sample;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Loan
        {
            public static readonly LoanCreateRequest Request = Sample.Loan.Request;

            public static readonly LoanUpdateRequest UpdateRequest = Sample.Loan.UpdateRequest;

            public static readonly Guid Id = Seeder.Loan.PersonalLoan1.Id;
        }
    }
}
