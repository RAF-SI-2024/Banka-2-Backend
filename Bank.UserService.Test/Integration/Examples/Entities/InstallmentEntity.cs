using Bank.Application.Requests;
using Bank.UserService.Database.Sample;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Installment
        {
            public static readonly InstallmentCreateRequest Request = Sample.Installment.Request;

            public static readonly InstallmentUpdateRequest UpdateRequest = Sample.Installment.UpdateRequest;

            public static readonly Guid InstallmentId = Seeder.Installment.AutoLoanInstallment1.Id;

            public static readonly Guid LoanId = Seeder.Loan.PersonalLoan1.Id;
        }
    }
}
