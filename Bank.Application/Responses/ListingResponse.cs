namespace Bank.Application.Responses;

public class ListingResponse
{
    public Guid                  Id         { get; set; }
    public string                Name       { get; set; }
    public string                Ticker     { get; set; }
    public StockExchangeResponse Exchange   { get; set; }
    public DateTime              CreatedAt  { get; set; }
    public DateTime              ModifiedAt { get; set; }
}

public class ListingCreateRequest
{
    public string Name     { get; set; }
    public string Ticker   { get; set; }
    public Guid   Exchange { get; set; }
}
