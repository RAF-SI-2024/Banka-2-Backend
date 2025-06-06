namespace Bank.Application.Responses;

public class StockExchangeResponse
{
    public required Guid                   Id       { get; set; }
    public required string                 Name     { get; set; }
    public required string                 Acronym  { get; set; }
    public required string                 MIC      { get; set; }
    public required string                 Polity   { get; set; }
    public required CurrencySimpleResponse Currency { get; set; }
    public required TimeSpan               TimeZone { get; set; }
    public required TimeSpan               MarketOpen { get; set; }

    public required TimeSpan               MarketClose   { get; set; }

    public required DateTime               CreatedAt  { get; set; }
    public required DateTime               ModifiedAt { get; set; }
}

public class ExchangeCreateRequest
{
    public required string   Name       { get; set; }
    public required string   Acronym    { get; set; }
    public required string   MIC        { get; set; }
    public required string   Polity     { get; set; }
    public required TimeSpan TimeZone   { get; set; }
    public required TimeSpan MarketOpen { get; set; }
    public required TimeSpan MarketClose { get; set; }
    public required Guid     CurrencyId { get; set; }
}
