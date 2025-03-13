namespace Bank.UserService.Models;

public class Installment
{
    public Guid              Id              { get; set; }
    public Guid              LoanId          { get; set; }
    public decimal           InterestRate    { get; set; }
    public DateTime          ExpectedDueDate { get; set; }
    public DateTime          ActualDueDate   { get; set; }
    public InstallmentStatus Status          { get; set; }
    public DateTime          CreatedAt       { get; set; }
    public DateTime          ModifiedAt      { get; set; }

    // Navigation property
    public Loan Loan { get; set; }
}
