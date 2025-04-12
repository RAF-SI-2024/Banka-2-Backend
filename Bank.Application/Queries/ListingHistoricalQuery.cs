namespace Bank.Application.Queries;

public class ListingHistoricalFilterQuery
{
    public Guid?     ListingId { get; set; }
    public DateTime? FromDate  { get; set; }
    public DateTime? ToDate    { get; set; }
}
