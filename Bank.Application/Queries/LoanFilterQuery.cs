namespace Bank.Application.Queries;

public class LoanFilterQuery
{
    public Guid?     LoanTypeId    { get; set; }
    public string?   AccountNumber { get; set; }
    public string?   LoanStatus    { get; set; }
    public DateTime? FromDate      { get; set; }
    public DateTime? ToDate        { get; set; }
}
