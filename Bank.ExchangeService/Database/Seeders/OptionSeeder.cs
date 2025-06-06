using System.Globalization;
using System.Web;

using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Models;
using Bank.ExchangeService.Repositories;

namespace Bank.ExchangeService.Database.Seeders;

using SecurityModel = Security;

public static partial class Seeder
{
    public static class Option
    {
        public static readonly SecurityModel AppleCallOption = new()
                                                               {
                                                                   Id              = Guid.Parse("c426aed1-9c27-4da1-aa51-6cf1045528d8"),
                                                                   OptionType      = OptionType.Call,
                                                                   StrikePrice     = 180.00m,
                                                                   OpenInterest    = 15234,
                                                                   SettlementDate  = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(3)),
                                                                   Name            = "AAPL Mar 2024 180 Call",
                                                                   Ticker          = "AAPL240315C00180000",
                                                                   StockExchangeId = StockExchange.Nasdaq.Id,
                                                                   SecurityType    = SecurityType.Option,
                                                               };

        public static readonly SecurityModel TeslaPutOption = new()
                                                              {
                                                                  Id              = Guid.Parse("d2accd8e-ed9a-4a51-96c8-f38a857bcc7f"),
                                                                  OptionType      = OptionType.Put,
                                                                  StrikePrice     = 240.00m,
                                                                  OpenInterest    = 8765,
                                                                  SettlementDate  = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(2)),
                                                                  Name            = "TSLA Feb 2024 240 Put",
                                                                  Ticker          = "TSLA240215P00240000",
                                                                  StockExchangeId = StockExchange.Nasdaq.Id,
                                                                  SecurityType    = SecurityType.Option,
                                                              };

        public static readonly SecurityModel MicrosoftCallOption = new()
                                                                   {
                                                                       Id              = Guid.Parse("c63d39fd-773b-4fb1-bfae-ae5af3ba8696"),
                                                                       OptionType      = OptionType.Call,
                                                                       StrikePrice     = 340.00m,
                                                                       OpenInterest    = 12456,
                                                                       SettlementDate  = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(1)),
                                                                       Name            = "MSFT Jan 2024 340 Call",
                                                                       Ticker          = "MSFT240119C00340000",
                                                                       StockExchangeId = StockExchange.ClearStreet.Id,
                                                                       SecurityType    = SecurityType.Option,
                                                                   };

        public static readonly SecurityModel AmazonPutOption = new()
                                                               {
                                                                   Id              = Guid.Parse("2bca4f12-be42-4d28-8320-11e78b3f4037"),
                                                                   OptionType      = OptionType.Put,
                                                                   StrikePrice     = 130.00m,
                                                                   OpenInterest    = 9876,
                                                                   SettlementDate  = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(4)),
                                                                   Name            = "AMZN Apr 2024 130 Put",
                                                                   Ticker          = "AMZN240419P00130000",
                                                                   StockExchangeId = StockExchange.ASX.Id,
                                                                   SecurityType    = SecurityType.Option,
                                                               };
    }
}

public static class OptionSeederExtension
{
    private static readonly HashSet<string> s_StockOptions =
    [
        "AAPL", "MSFT", "AMZN", "GOOGL", "META", "TSLA", "BRK.B", "NVDA", "JPM", "JNJ",
        "V", "PG", "UNH", "HD", "DIS", "PYPL", "MA", "INTC", "VZ", "NFLX",
        "ADBE", "CMCSA", "PFE", "KO", "MRK", "PEP", "T", "XOM", "CSCO", "ABT",
        "ABBV", "CRM", "NKE", "WMT", "MCD", "IBM", "QCOM", "ORCL", "CVX", "MDT",
        "BA", "HON", "COST", "AMGN", "TMO", "DHR", "LIN", "AVGO", "ACN", "PM",
        "TXN", "UNP", "LOW", "NEE", "UPS", "MS", "CAT", "LMT", "ISRG", "SPGI",
        "NOW", "BLK", "AMD", "DE", "SYK", "GS", "PLD", "BKNG", "ADP", "CCI",
        "ZTS", "SCHW", "CB", "TGT", "CME", "USB", "AMT", "FIS", "MO", "MDLZ",
        "GILD", "VRTX", "REGN", "TFC", "CI", "EQIX", "SO", "HUM", "ITW", "WM",
        "D", "APD", "SBUX", "EL", "CL", "NSC", "MMC", "EMR", "ADI", "ETN"
    ];

    public static async Task SeedOptionHardcoded(this DatabaseContext context)
    {
        if (context.Securities.Any(security => security.SecurityType == SecurityType.Option))
            return;

        await context.Securities.AddRangeAsync(Seeder.Option.AmazonPutOption, Seeder.Option.AppleCallOption, Seeder.Option.MicrosoftCallOption);

        await context.SaveChangesAsync();
    }

    public static async Task SeedOptionsAndQuotes(this DatabaseContext context, HttpClient httpClient, ISecurityRepository securityRepository, IQuoteRepository quoteRepository)
    {
        if (context.Securities.Any(security => security.SecurityType == SecurityType.Option))
            return;

        var (apiKey, apiSecret) = Configuration.Security.Keys.AlpacaApiKeyAndSecret;

        var stocks = (await securityRepository.FindAll(SecurityType.Stock)).Where(stock => s_StockOptions.Contains(stock.Ticker))
                                                                           .Select(security => security.ToStock())
                                                                           .ToList();

        var     toDate     = Configuration.Security.Option.ToDateTime;
        string? nextPage   = null;
        var     securities = new List<SecurityModel>();
        var     quotes     = new List<Quote>();
        var     query      = HttpUtility.ParseQueryString(string.Empty);
        query["feed"]  = "indicative";
        query["limit"] = "1000";

        if (toDate != DateTime.MaxValue.ToString(CultureInfo.InvariantCulture))
            query["expiration_date_lte"] = toDate;

        foreach (var stock in stocks)
        {
            do
            {
                if (!string.IsNullOrEmpty(nextPage))
                    query["page_token"] = nextPage;
                else
                    query.Remove("page_token");

                var request = new HttpRequestMessage
                              {
                                  Method     = HttpMethod.Get,
                                  RequestUri = new Uri($"{Configuration.Security.Option.OptionChainApi}/{stock.Ticker}?{query}"),
                                  Headers =
                                  {
                                      { "accept", "application/json" },
                                      { "APCA-API-KEY-ID", apiKey },
                                      { "APCA-API-SECRET-KEY", apiSecret },
                                  },
                              };

                using var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    break;

                var body = await response.Content.ReadFromJsonAsync<FetchOptionsResponse>();

                if (body == null)
                    return;

                foreach (var pair in body.Snapshots)
                {
                    if (pair.Value.DailyBar == null || pair.Value.LatestQuote == null)
                        continue;

                    var (expirationDate, strikePrice, optionType) = ParseOptionTracker(pair.Key);

                    var security = pair.Value.ToOption(stock, pair.Key, expirationDate, strikePrice, optionType)
                                       .ToSecurity();

                    securities.Add(security);
                    quotes.Add(pair.Value.ToQuote(security));
                }

                nextPage = body.NextPage;
            } while (!string.IsNullOrEmpty(nextPage));
        }

        await securityRepository.CreateSecurities(securities);
        await quoteRepository.CreateQuotes(quotes);
    }

    private static (DateOnly ExpirationDate, decimal StrikePrice, OptionType OptionType) ParseOptionTracker(string optionTracker)
    {
        var withoutStockTracker = optionTracker[optionTracker.IndexOfAny("0123456789".ToCharArray())..];

        var year           = int.Parse(withoutStockTracker[..2]) + 2000;
        var month          = int.Parse(withoutStockTracker.Substring(2, 2));
        var day            = int.Parse(withoutStockTracker.Substring(4, 2));
        var expirationDate = new DateOnly(year, month, day);

        var optionType = withoutStockTracker[6] == 'C' ? OptionType.Call : OptionType.Put;

        var strikePriceStr = withoutStockTracker[7..];
        var strikePrice    = decimal.Parse(strikePriceStr) / 1000m;

        return (expirationDate, strikePrice, optionType);
    }
}
