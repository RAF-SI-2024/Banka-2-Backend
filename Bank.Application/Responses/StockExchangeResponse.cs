namespace Bank.Application.Responses;

public class StockExchangeResponse
{
    public Guid             Id         { get; set; }
    public string           Name       { get; set; }
    public string           Acronym    { get; set; }
    public string           MIC        { get; set; }
    public string           Polity     { get; set; }
    public CurrencyResponse Currency   { get; set; }
    public TimeSpan         TimeZone   { get; set; }
    public DateTime         CreatedAt  { get; set; }
    public DateTime         ModifiedAt { get; set; }
}

public class ExchangeCreateRequest
{
    public string   Name       { get; set; }
    public string   Acronym    { get; set; }
    public string   MIC        { get; set; }
    public string   Polity     { get; set; }
    public TimeSpan TimeZone   { get; set; }
    public Guid     CurrencyId { get; set; }
}
