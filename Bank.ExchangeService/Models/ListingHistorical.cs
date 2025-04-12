namespace Bank.ExchangeService.Model;

public class ListingHistorical
{
    public          Guid     Id           { get; set; }
    public          Guid     ListingId    { get; set; }
    public required decimal  ClosingPrice { get; set; }
    public required decimal  HighPrice    { get; set; }
    public required decimal  LowPrice     { get; set; }
    public required decimal  PriceChange  { get; set; }
    public required int      Volume       { get; set; }
    public required DateTime CreatedAt    { get; set; }
    public required DateTime ModifiedAt   { get; set; }
    public virtual  Listing? Listing      { get; set; }
}
