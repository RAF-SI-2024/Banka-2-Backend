namespace Bank.Application.Responses;

public class InstallmentResponse
{
    public Guid         Id              { get; set; }
    public LoanResponse Loan            { get; set; }
    public decimal      InterestRate    { get; set; }
    public DateOnly     ExpectedDueDate { get; set; }
    public DateOnly     ActualDueDate   { get; set; }
    public int          Status          { get; set; }
    public DateTime     CreatedAt       { get; set; }
    public DateTime     ModifiedAt      { get; set; }
}
