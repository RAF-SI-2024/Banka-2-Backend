using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Installment
    {
        public static readonly InstallmentCreateRequest DefaultCreateRequest = new()
                                                                               {
                                                                                   InstallmentId   = Constant.Id,
                                                                                   LoanId          = Constant.Id,
                                                                                   InterestRate    = Constant.Commission,
                                                                                   ExpectedDueDate = Constant.CreationDate,
                                                                                   ActualDueDate   = Constant.CreationDate,
                                                                                   Status          = Constant.InstallmentStatus,
                                                                               };

        public static readonly InstallmentUpdateRequest DefaultUpdateRequest = new()
                                                                               {
                                                                                   ActualDueDate = Constant.CreatedAt,
                                                                                   Status        = Constant.InstallmentStatus,
                                                                               };

        public static readonly InstallmentResponse DefaultResponse = new()
                                                                     {
                                                                         Id              = Constant.Id,
                                                                         Loan            = Loan.DefaultResponse,
                                                                         InterestRate    = Constant.Commission,
                                                                         ExpectedDueDate = Constant.CreationDate,
                                                                         ActualDueDate   = Constant.CreationDate,
                                                                         Status          = Constant.InstallmentStatus,
                                                                         CreatedAt       = Constant.CreatedAt,
                                                                         ModifiedAt      = Constant.ModifiedAt,
                                                                     };
    }
}
