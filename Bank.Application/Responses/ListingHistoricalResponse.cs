namespace Bank.Application.Responses;

public class ListingHistoricalResponse
{
    public Guid            Id           { get; set; }
    public ListingResponse Listing      { get; set; }
    public decimal         ClosingPrice { get; set; }
    public decimal         HighPrice    { get; set; }
    public decimal         LowPrice     { get; set; }
    public decimal         PriceChange  { get; set; }
    public int             Volume       { get; set; }
    public DateTime        CreatedAt    { get; set; }
    public DateTime        ModifiedAt   { get; set; }
}

public class ListingHistoricalCreateRequest
{
    public Guid    Listing      { get; set; }
    public decimal ClosingPrice { get; set; }
    public decimal HighPrice    { get; set; }
    public decimal LowPrice     { get; set; }
    public int     Volume       { get; set; }
}
