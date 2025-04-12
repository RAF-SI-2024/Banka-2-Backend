namespace Bank.ExchangeService.Model;

public class StockExchange
{
    public required Guid     Id         { get; set; }
    public required string   Name       { get; set; }
    public required string   Acronym    { get; set; }
    public          string   MIC        { get; set; }
    public          string   Polity     { get; set; }
    public          Guid     CurrencyId { get; set; }
    public          TimeSpan TimeZone   { get; set; }
    public required DateTime CreatedAt  { get; set; }
    public required DateTime ModifiedAt { get; set; }
}
