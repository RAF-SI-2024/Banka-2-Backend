using Bank.Application.Responses;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Database.Examples;

public static partial class Example
{
    public static class Quote
    {
        public static readonly QuoteLatestSimpleResponse LatestSimpleResponse = new()
                                                                                {
                                                                                    SecurityTicker = "AAPL",
                                                                                    AskPrice       = Seeder.Quote.StockAmazonQuote.AskPrice,
                                                                                    BidPrice       = Seeder.Quote.StockAmazonQuote.BidPrice,
                                                                                    HighPrice      = Seeder.Quote.StockAmazonQuote.HighPrice,
                                                                                    LowPrice       = Seeder.Quote.StockAmazonQuote.LowPrice,
                                                                                    Volume         = Seeder.Quote.StockAmazonQuote.Volume,
                                                                                    CreatedAt      = DateTime.UtcNow,
                                                                                    ModifiedAt     = DateTime.UtcNow
                                                                                };

        public static readonly QuoteDailySimpleResponse DailySimpleResponse = new()
                                                                              {
                                                                                  HighPrice  = Seeder.Quote.StockAmazonQuote.AskPrice,
                                                                                  LowPrice   = Seeder.Quote.StockAmazonQuote.BidPrice,
                                                                                  OpenPrice  = Seeder.Quote.StockAmazonQuote.HighPrice,
                                                                                  ClosePrice = Seeder.Quote.StockAmazonQuote.LowPrice,
                                                                                  Volume     = Seeder.Quote.StockAmazonQuote.Volume,
                                                                                  CreatedAt  = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                                                                                  ModifiedAt = DateOnly.FromDateTime(DateTime.UtcNow.Date)
                                                                              };

        public static readonly QuoteSimpleResponse SimpleResponse = new()
                                                                    {
                                                                        Id            = Seeder.Quote.StockAmazonQuote.Id,
                                                                        HighPrice     = Seeder.Quote.StockAmazonQuote.AskPrice,
                                                                        LowPrice      = Seeder.Quote.StockAmazonQuote.BidPrice,
                                                                        AskPrice      = Seeder.Quote.StockAmazonQuote.HighPrice,
                                                                        BidPrice      = Seeder.Quote.StockAmazonQuote.LowPrice,
                                                                        Volume        = Seeder.Quote.StockAmazonQuote.Volume,
                                                                        CreatedAt     = DateTime.UtcNow,
                                                                        ModifiedAt    = DateTime.UtcNow,
                                                                        ContractCount = 1
                                                                    };
    }
}
