namespace Bank.Application.Queries;

public class StockExchangeFilterQuery
{
    public string? Name    { get; set; }
    public string? Acronym { get; set; }
    public string? MIC     { get; set; }
    public string? Polity  { get; set; }
}
