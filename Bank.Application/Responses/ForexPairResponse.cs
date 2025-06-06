using System.Globalization;
using System.Text.Json.Serialization;

using Bank.Application.Domain;

namespace Bank.Application.Responses;

public class ForexPairResponse
{
    public required Guid                      Id                           { get; set; }
    public required Liquidity                 Liquidity                    { get; set; }
    public required CurrencySimpleResponse    BaseCurrency                 { get; set; }
    public required CurrencySimpleResponse    QuoteCurrency                { get; set; }
    public required decimal                   MaintenanceDecimal           { get; set; }
    public required decimal                   ExchangeRate                 { get; set; }
    public required string                    Name                         { get; set; }
    public required string                    Ticker                       { get; set; }
    public required decimal                   HighPrice                    { get; set; }
    public required decimal                   LowPrice                     { get; set; }
    public required decimal                   AskPrice                     { get; set; }
    public required decimal                   BidPrice                     { get; set; }
    public required int                       AskSize                      { get; set; }
    public required int                       BidSize                      { get; set; }
    public required int                       ContractCount                { get; set; }
    public required decimal                   PriceChangeInInterval        { get; set; }
    public required decimal                   PriceChangePercentInInterval { get; set; }
    public required DateTime                  CreatedAt                    { get; set; }
    public required DateTime                  ModifiedAt                   { get; set; }
    public required StockExchangeResponse     StockExchange                { get; set; }
    public required List<QuoteSimpleResponse> Quotes                       { get; set; } = [];
}

public class ForexPairDailyResponse
{
    public required Guid                           Id                           { get; set; }
    public required Liquidity                      Liquidity                    { get; set; }
    public required CurrencySimpleResponse         BaseCurrency                 { get; set; }
    public required CurrencySimpleResponse         QuoteCurrency                { get; set; }
    public required decimal                        MaintenanceDecimal           { get; set; }
    public required decimal                        ExchangeRate                 { get; set; }
    public required string                         Name                         { get; set; }
    public required string                         Ticker                       { get; set; }
    public required decimal                        HighPrice                    { get; set; }
    public required decimal                        LowPrice                     { get; set; }
    public required decimal                        OpeningPrice                 { get; set; }
    public required decimal                        ClosePrice                   { get; set; }
    public required decimal                        PriceChangeInInterval        { get; set; }
    public required decimal                        PriceChangePercentInInterval { get; set; }
    public required DateTime                       CreatedAt                    { get; set; }
    public required DateTime                       ModifiedAt                   { get; set; }
    public required StockExchangeResponse          StockExchange                { get; set; }
    public required List<QuoteDailySimpleResponse> Quotes                       { get; set; } = [];
}

public class ForexPairSimpleResponse
{
    public required Guid                   Id                           { get; set; }
    public required Liquidity              Liquidity                    { get; set; }
    public required CurrencySimpleResponse BaseCurrency                 { get; set; }
    public required CurrencySimpleResponse QuoteCurrency                { get; set; }
    public required decimal                ExchangeRate                 { get; set; }
    public required decimal                MaintenanceDecimal           { get; set; }
    public required string                 Name                         { get; set; }
    public required string                 Ticker                       { get; set; }
    public required decimal                HighPrice                    { get; set; }
    public required decimal                LowPrice                     { get; set; }
    public required decimal                AskPrice                     { get; set; }
    public required decimal                BidPrice                     { get; set; }
    public required int                    AskSize                      { get; set; }
    public required int                    BidSize                      { get; set; }
    public required int                    ContractCount                { get; set; }
    public required decimal                PriceChangeInInterval        { get; set; }
    public required decimal                PriceChangePercentInInterval { get; set; }
    public required decimal                Price                        { get; set; }
    public required DateTime               CreatedAt                    { get; set; }
    public required DateTime               ModifiedAt                   { get; set; }
}

public class FetchForexPairLatestResponse
{
    [JsonPropertyName("1. From_Currency Code")]
    public required string CurrencyCodeFrom { get; set; }

    [JsonPropertyName("3. To_Currency Code")]
    public required string CurrencyCodeTo { get; set; }

    [JsonPropertyName("5. Exchange Rate")]
    public required string ExchangeRateString { get; set; }

    [JsonPropertyName("6. Last Refreshed")]
    public required string DateString { get; set; }

    [JsonPropertyName("8. Bid Price")]
    public required string BidPriceString { get; set; }

    [JsonPropertyName("9. Ask Price")]
    public required string AskPriceString { get; set; }

    public decimal ExchangeRate => decimal.Parse(ExchangeRateString, CultureInfo.InvariantCulture);

    public decimal BidPrice => decimal.Parse(BidPriceString, CultureInfo.InvariantCulture);

    public decimal AskPrice => decimal.Parse(AskPriceString, CultureInfo.InvariantCulture);

    public DateTime Date => DateTime.ParseExact(DateString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
}

public class FetchForexPairQuotesResponse
{
    [JsonPropertyName("Time Series FX (Daily)")]
    public required Dictionary<string, FetchForexPairQuoteResponse> Quotes { get; set; }
}

public class FetchForexPairQuoteResponse
{
    [JsonPropertyName("1. open")]
    public required string OpenString { get; set; }

    [JsonPropertyName("2. high")]
    public required string HighString { get; set; }

    [JsonPropertyName("3. low")]
    public required string LowString { get; set; }

    [JsonPropertyName("4. close")]
    public required string CloseString { get; set; }

    public decimal Open => decimal.Parse(OpenString, CultureInfo.InvariantCulture);

    public decimal High => decimal.Parse(HighString, CultureInfo.InvariantCulture);

    public decimal Low => decimal.Parse(LowString, CultureInfo.InvariantCulture);

    public decimal Close => decimal.Parse(CloseString, CultureInfo.InvariantCulture);
}
