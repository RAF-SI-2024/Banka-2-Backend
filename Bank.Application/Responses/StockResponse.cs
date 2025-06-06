using System.Text.Json.Serialization;

namespace Bank.Application.Responses;

public class StockResponse
{
    public required Guid                      Id                           { get; set; }
    public required string                    Name                         { get; set; }
    public required string                    Ticker                       { get; set; }
    public required decimal                   HighPrice                    { get; set; }
    public required decimal                   LowPrice                     { get; set; }
    public required decimal                   AskPrice                     { get; set; }
    public required int                       AskSize                      { get; set; }
    public required decimal                   BidPrice                     { get; set; }
    public required int                       BidSize                      { get; set; }
    public required int                       ContractCount                { get; set; }
    public required long                      Volume                       { get; set; }
    public required decimal                   PriceChangeInInterval        { get; set; }
    public required decimal                   PriceChangePercentInInterval { get; set; }
    public required DateTime                  CreatedAt                    { get; set; }
    public required DateTime                  ModifiedAt                   { get; set; }
    public required StockExchangeResponse     StockExchange                { get; set; }
    public required List<QuoteSimpleResponse> Quotes                       { get; set; } = [];
}

public class StockDailyResponse
{
    public required Guid                           Id                           { get; set; }
    public required string                         Name                         { get; set; }
    public required string                         Ticker                       { get; set; }
    public required decimal                        HighPrice                    { get; set; }
    public required decimal                        LowPrice                     { get; set; }
    public required decimal                        OpenPrice                    { get; set; }
    public required decimal                        ClosePrice                   { get; set; }
    public required long                           Volume                       { get; set; }
    public required decimal                        PriceChangeInInterval        { get; set; }
    public required decimal                        PriceChangePercentInInterval { get; set; }
    public required DateTime                       CreatedAt                    { get; set; }
    public required DateTime                       ModifiedAt                   { get; set; }
    public required StockExchangeResponse          StockExchange                { get; set; }
    public required List<QuoteDailySimpleResponse> Quotes                       { get; set; } = [];
}

public class StockSimpleResponse
{
    public required Guid     Id                           { get; set; }
    public required string   Name                         { get; set; }
    public required string   Ticker                       { get; set; }
    public required decimal  HighPrice                    { get; set; }
    public required decimal  LowPrice                     { get; set; }
    public required decimal  AskPrice                     { get; set; }
    public required int      AskSize                      { get; set; }
    public required decimal  BidPrice                     { get; set; }
    public required int      BidSize                      { get; set; }
    public required int      ContractCount                { get; set; }
    public required long     Volume                       { get; set; }
    public required decimal  PriceChangeInInterval        { get; set; }
    public required decimal  PriceChangePercentInInterval { get; set; }
    public required DateTime CreatedAt                    { get; set; }
    public required DateTime ModifiedAt                   { get; set; }
}

public class FetchStockBarsResponse
{
    [JsonPropertyName("next_page_token")]
    public string? NextPage { get; set; }

    public required Dictionary<string, List<FetchStockBarOneResponse>> Bars { get; set; }
}

public class FetchStockBarOneResponse
{
    [JsonPropertyName("c")]
    public required decimal ClosePrice { get; set; }

    [JsonPropertyName("h")]
    public required decimal HighPrice { get; set; }

    [JsonPropertyName("l")]
    public required decimal LowPrice { get; set; }

    [JsonPropertyName("n")]
    public required int NumberOfTradesInInterval { get; set; }

    [JsonPropertyName("o")]
    public required decimal OpeningPrice { get; set; }

    [JsonPropertyName("t")]
    public required DateTime Date { get; set; }

    [JsonPropertyName("v")]
    public required long NumberOfSharesInInterval { get; set; }

    [JsonPropertyName("vw")]
    public required decimal VolumeWeightedAveragePrice { get; set; }
}

public class FetchStockQuoteOneResponse
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

    [JsonPropertyName("t")]
    public required DateTime Date { get; set; }
}

public class FetchStockSnapshotResponse
{
    public FetchStockBarOneResponse?   DailyBar    { get; set; }
    public FetchStockQuoteOneResponse? LatestQuote { get; set; }
    public FetchStockBarOneResponse?   MinuteBar   { get; set; }
}

public class FetchStockResponse
{
    [JsonPropertyName("exchange")]
    public required string StockExchangeAcronym { get; set; }

    [JsonPropertyName("symbol")]
    public required string Ticker { get; set; }

    public required string Name     { get; set; }
    public required bool   Tradable { get; set; }
}
