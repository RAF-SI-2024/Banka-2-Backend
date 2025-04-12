namespace Bank.Application.Queries;

public class ListingFilterQuery
{
    public string? Name       { get; set; }
    public string? Ticker     { get; set; }
    public Guid?   ExchangeId { get; set; }
}
