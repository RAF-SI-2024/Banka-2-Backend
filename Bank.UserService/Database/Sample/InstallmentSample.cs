using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
{
    public static class Installment
    {
        public static readonly InstallmentCreateRequest Request = new()
                                                            {
                                                                InstallmentId   = Guid.Parse("a52cbe51-d29e-486a-b7dd-079aa315883f"),
                                                                LoanId          = Seeder.Loan.PersonalLoan1.Id,
                                                                InterestRate    = 5.0m,
                                                                ExpectedDueDate = new(2025, 6, 15),
                                                                ActualDueDate   = new(2025, 6, 20),
                                                                Status          = InstallmentStatus.Pending
                                                            };

        public static readonly InstallmentUpdateRequest UpdateRequest = new()
                                                                        {
                                                                            ActualDueDate = new(2025, 6, 15),
                                                                            Status        = InstallmentStatus.Paid
                                                                        };
    }
}
