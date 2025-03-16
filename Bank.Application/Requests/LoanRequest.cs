namespace Bank.Application.Requests;

public class LoanRequest
{
    public Guid    TypeId       { get; set; }
    public Guid    AccountId    { get; set; }
    public decimal Amount       { get; set; }
    public int     Period       { get; set; }
    public Guid    CurrencyId   { get; set; }
    public int     InterestType { get; set; }
}
