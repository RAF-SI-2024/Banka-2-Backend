namespace Bank.Application.Queries;

public class ExchangeFilterQuery
{
    public Guid     CurrencyId   { set; get; }
    public string?  CurrencyCode { set; get; }
    public DateOnly Date         { set; get; }
}
