using Bank.Application.Domain;

namespace Bank.Application.Requests;

public class InstallmentCreateRequest
{
    public required Guid              InstallmentId;
    public required Guid              LoanId          { get; set; }
    public required decimal           InterestRate    { get; set; }
    public required DateOnly          ExpectedDueDate { get; set; }
    public required DateOnly          ActualDueDate   { get; set; }
    public required InstallmentStatus Status          { get; set; }
}

public class InstallmentUpdateRequest
{
    public DateTime?          ActualDueDate { get; set; }
    public InstallmentStatus? Status        { get; set; }
}
