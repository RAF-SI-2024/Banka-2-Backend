namespace Bank.ExchangeService.Model;

public class Listing
{
    public          Guid           Id              { get; set; }
    public required string         Name            { get; set; }
    public required string         Ticker          { get; set; }
    public          Guid           StockExchangeId { get; set; }
    public required DateTime       CreatedAt       { get; set; }
    public required DateTime       ModifiedAt      { get; set; }
    public virtual  StockExchange? StockExchange   { get; set; }
}
