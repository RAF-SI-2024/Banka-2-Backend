using Bank.Application.Domain;

namespace Bank.Application.Responses;

public class InstallmentResponse
{
    public Guid              Id              { get; set; }
    public LoanResponse      Loan            { get; set; }
    public decimal           InterestRate    { get; set; }
    public DateOnly          ExpectedDueDate { get; set; }
    public DateOnly          ActualDueDate   { get; set; }
    public InstallmentStatus Status          { get; set; }
    public DateTime          CreatedAt       { get; set; }
    public DateTime          ModifiedAt      { get; set; }

    public decimal Amount { get; set; }
}
