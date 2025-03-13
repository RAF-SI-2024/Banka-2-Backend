namespace Bank.Application.Queries;

public class ExchangeRateFilterQuery
{
    public Guid?   CurrencyId   { set; get; }
    public string? CurrencyCode { set; get; }
}
