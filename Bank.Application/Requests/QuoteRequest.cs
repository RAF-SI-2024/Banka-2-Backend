namespace Bank.Application.Requests;

public class FakeQuoteRequest
{
    public required Guid    SecurityId        { get; set; }
    public required decimal AskPrice          { get; set; }
    public required decimal BidPrice          { get; set; }
    public required int     AskSize           { get; set; }
    public required int     BidSize           { get; set; }
    public          decimal ImpliedVolatility { get; set; }
    public required long    Volume            { get; set; }
    public required int     ContractCount     { get; set; }

    public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
}
