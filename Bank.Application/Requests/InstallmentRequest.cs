namespace Bank.Application.Requests;

public class InstallmentRequest
{
    public required Guid     InstallmentId;
    public required Guid     LoanId          { get; set; }
    public required decimal  InterestRate    { get; set; }
    public required DateOnly ExpectedDueDate { get; set; }
    public required DateOnly ActualDueDate   { get; set; }
    public required int      Status          { get; set; }
}
