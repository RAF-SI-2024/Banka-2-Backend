using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class InstallmentMapper
{
    public static Installment ToInstallment(this InstallmentCreateRequest createRequest)
    {
        return new Installment
               {
                   Id              = Guid.NewGuid(),
                   LoanId          = createRequest.LoanId,
                   InterestRate    = createRequest.InterestRate,
                   ExpectedDueDate = DateTime.SpecifyKind(createRequest.ExpectedDueDate.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc),
                   ActualDueDate   = DateTime.SpecifyKind(createRequest.ActualDueDate.ToDateTime(TimeOnly.MinValue),   DateTimeKind.Utc),
                   Status          = createRequest.Status,
                   CreatedAt       = DateTime.UtcNow,
                   ModifiedAt      = DateTime.UtcNow
               };
    }

    public static InstallmentResponse ToResponse(this Installment installment)
    {
        return new InstallmentResponse
               {
                   Id              = installment.Id,
                   Loan            = installment.Loan.ToLoanResponse(),
                   InterestRate    = installment.InterestRate,
                   ExpectedDueDate = DateOnly.FromDateTime(installment.ExpectedDueDate),
                   ActualDueDate   = DateOnly.FromDateTime(installment.ActualDueDate),
                   Status          = installment.Status,
                   CreatedAt       = installment.CreatedAt,
                   ModifiedAt      = installment.ModifiedAt
               };
    }

    public static Installment Update(this Installment installment, InstallmentUpdateRequest updateRequest)
    {
        installment.ActualDueDate = updateRequest.ActualDueDate ?? installment.ActualDueDate;
        installment.Status        = updateRequest.Status        ?? installment.Status;
        installment.ModifiedAt    = DateTime.UtcNow;

        return installment;
    }
}
