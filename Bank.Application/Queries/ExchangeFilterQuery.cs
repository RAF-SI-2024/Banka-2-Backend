namespace Bank.Application.Queries;

public class ExchangeFilterQuery
{
    public Guid     CurrencyId   { set; get; }
    public string?  CurrencyCode { set; get; }
    public DateOnly Date         { set; get; }
}

public class ExchangeBetweenQuery
{
    public required string CurrencyFromCode { set; get; }
    public required string CurrencyToCode   { set; get; }
}
