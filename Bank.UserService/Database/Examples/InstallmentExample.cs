using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Examples;

public static partial class Example
{
    public static class Installment
    {
        public static readonly InstallmentCreateRequest CreateRequest = new()
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
                                                                            ActualDueDate = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                                                                            Status        = InstallmentStatus.Paid
                                                                        };

        public static readonly InstallmentResponse Response = new()
                                                              {
                                                                  Id              = CreateRequest.InstallmentId,
                                                                  Loan            = null!,
                                                                  InterestRate    = CreateRequest.InterestRate,
                                                                  ExpectedDueDate = CreateRequest.ExpectedDueDate,
                                                                  ActualDueDate   = CreateRequest.ActualDueDate,
                                                                  Status          = CreateRequest.Status,
                                                                  CreatedAt       = DateTime.UtcNow,
                                                                  ModifiedAt      = DateTime.UtcNow,
                                                              };
    }
}
