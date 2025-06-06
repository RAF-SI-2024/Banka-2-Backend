namespace Bank.Application.Responses;

public class QuoteSimpleResponse
{
    public required Guid     Id            { get; set; }
    public required decimal  HighPrice     { get; set; }
    public required decimal  LowPrice      { get; set; }
    public required decimal  AskPrice      { get; set; }
    public required decimal  BidPrice      { get; set; }
    public required long     Volume        { get; set; }
    public required long     ContractCount { get; set; }
    public required DateTime CreatedAt     { get; set; }
    public required DateTime ModifiedAt    { get; set; }
}

public class QuoteLatestSimpleResponse
{
    public required string   SecurityTicker { get; set; }
    public required decimal  AskPrice       { get; set; }
    public required decimal  BidPrice       { get; set; }
    public required decimal  HighPrice      { get; set; }
    public required decimal  LowPrice       { get; set; }
    public required long     Volume         { get; set; }
    public required DateTime CreatedAt      { get; set; }
    public required DateTime ModifiedAt     { get; set; }
}

public class QuoteDailySimpleResponse
{
    public required decimal  HighPrice  { get; set; }
    public required decimal  LowPrice   { get; set; }
    public required decimal  OpenPrice  { get; set; }
    public required decimal  ClosePrice { get; set; }
    public required long     Volume     { get; set; }
    public required DateOnly CreatedAt  { get; set; }
    public required DateOnly ModifiedAt { get; set; }
}
