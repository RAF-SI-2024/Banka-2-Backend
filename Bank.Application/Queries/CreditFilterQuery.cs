namespace Bank.Application.Queries;

public class CreditFilterQuery
{
    public Guid?     CreditTypeId  { get; set; }
    public string?   AccountNumber { get; set; }
    public string?   CreditStatus  { get; set; }
    public DateTime? FromDate      { get; set; }
    public DateTime? ToDate        { get; set; }
}
