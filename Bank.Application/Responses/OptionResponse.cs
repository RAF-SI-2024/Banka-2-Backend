using System.Text.Json.Serialization;

using Bank.Application.Domain;

namespace Bank.Application.Responses;

public class OptionResponse
{
    public required Guid                      Id                           { get; set; }
    public required decimal                   StrikePrice                  { get; set; }
    public required decimal                   ImpliedVolatility            { get; set; }
    public required DateOnly                  SettlementDate               { get; set; }
    public required string                    Name                         { get; set; }
    public required string                    Ticker                       { get; set; }
    public required OptionType                OptionType                   { get; set; }
    public required decimal                   HighPrice                    { get; set; }
    public required decimal                   LowPrice                     { get; set; }
    public required long                      Volume                       { get; set; }
    public required decimal                   PriceChangeInInterval        { get; set; }
    public required decimal                   PriceChangePercentInInterval { get; set; }
    public required decimal                   AskPrice                     { get; set; }
    public required int                       AskSize                      { get; set; }
    public required decimal                   BidPrice                     { get; set; }
    public required int                       BidSize                      { get; set; }
    public required int                       ContractCount                { get; set; }
    public required DateTime                  CreatedAt                    { get; set; }
    public required DateTime                  ModifiedAt                   { get; set; }
    public required StockExchangeResponse     StockExchange                { get; set; }
    public required List<QuoteSimpleResponse> Quotes                       { get; set; } = [];
}

public class OptionDailyResponse
{
    public required Guid                           Id                           { get; set; }
    public required decimal                        StrikePrice                  { get; set; }
    public required decimal                        ImpliedVolatility            { get; set; }
    public required DateOnly                       SettlementDate               { get; set; }
    public required string                         Name                         { get; set; }
    public required string                         Ticker                       { get; set; }
    public required OptionType                     OptionType                   { get; set; }
    public required decimal                        HighPrice                    { get; set; }
    public required decimal                        LowPrice                     { get; set; }
    public required decimal                        OpeningPrice                 { get; set; }
    public required decimal                        ClosePrice                   { get; set; }
    public required long                           Volume                       { get; set; }
    public required decimal                        PriceChangeInInterval        { get; set; }
    public required decimal                        PriceChangePercentInInterval { get; set; }
    public required DateTime                       CreatedAt                    { get; set; }
    public required DateTime                       ModifiedAt                   { get; set; }
    public required StockExchangeResponse          StockExchange                { get; set; }
    public required List<QuoteDailySimpleResponse> Quotes                       { get; set; } = [];
}

public class OptionSimpleResponse
{
    public required Guid       Id                           { get; set; }
    public required decimal    StrikePrice                  { get; set; }
    public required decimal    ImpliedVolatility            { get; set; }
    public required DateOnly   SettlementDate               { get; set; }
    public required string     Name                         { get; set; }
    public required string     Ticker                       { get; set; }
    public required OptionType OptionType                   { get; set; }
    public required decimal    HighPrice                    { get; set; }
    public required decimal    LowPrice                     { get; set; }
    public required long       Volume                       { get; set; }
    public required decimal    PriceChange                  { get; set; }
    public required decimal    PriceChangeInInterval        { get; set; }
    public required decimal    PriceChangePercentInInterval { get; set; }
    public required decimal    AskPrice                     { get; set; }
    public required int        AskSize                      { get; set; }
    public required decimal    BidPrice                     { get; set; }
    public required int        BidSize                      { get; set; }
    public required int        ContractCount                { get; set; }
    public required DateTime   CreatedAt                    { get; set; }
    public required DateTime   ModifiedAt                   { get; set; }
}

public class FetchOptionOneBarResponse
{
    [JsonPropertyName("c")]
    public required decimal ClosingPrice { get; set; }

    [JsonPropertyName("h")]
    public required decimal HighPrice { get; set; }

    [JsonPropertyName("l")]
    public required decimal LowPrice { get; set; }

    [JsonPropertyName("n")]
    public required int TradeCount { get; set; }

    [JsonPropertyName("o")]
    public required decimal OpeningPrice { get; set; }

    [JsonPropertyName("t")]
    public required DateTime TimeStamp { get; set; }

    [JsonPropertyName("v")]
    public required int Volume { get; set; }

    [JsonPropertyName("vw")]
    public required double VolumeWeightedAveragePrice { get; set; }
}

public class FetchOptionOneQuoteResponse
{
    [JsonPropertyName("ap")]
    public required decimal AskPrice { get; set; }

    [JsonPropertyName("as")]
    public required int AskSize { get; set; }

    [JsonPropertyName("ax")]
    public required string AskExchange { get; set; }

    [JsonPropertyName("bp")]
    public required decimal BidPrice { get; set; }

    [JsonPropertyName("bs")]
    public required int BidSize { get; set; }

    [JsonPropertyName("bx")]
    public required string BidExchange { get; set; }

    [JsonPropertyName("c")]
    public required string QuoteCondition { get; set; }

    [JsonPropertyName("t")]
    public required DateTime TimeStamp { get; set; }
}

public class FetchOptionOneResponse
{
    public FetchOptionOneBarResponse?   DailyBar          { get; set; }
    public FetchOptionOneQuoteResponse? LatestQuote       { get; set; }
    public decimal                      ImpliedVolatility { get; set; }
}

public class FetchOptionsResponse
{
    [JsonPropertyName("next_page_token")]
    public string? NextPage { get; set; }

    public required Dictionary<string, FetchOptionOneResponse> Snapshots { get; set; }
}
