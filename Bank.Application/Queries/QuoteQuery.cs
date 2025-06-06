using Bank.Application.Domain;

namespace Bank.Application.Queries;

public class QuoteFilterQuery
{
    public Guid StockExchangeId { get; set; }

    public required QuoteIntervalType Interval { get; set; }
    public          string?           Name     { get; set; }
    public          string?           Ticker   { get; set; }
}

public class QuoteFilterIntervalQuery
{
    public required QuoteIntervalType Interval { get; set; }
}
