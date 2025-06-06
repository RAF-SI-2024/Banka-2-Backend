using Bank.Application.Responses;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Database.Examples;

public static partial class Example
{
    public static class StockExchange
    {
        public static readonly ExchangeCreateRequest CreateRequest = new()
                                                                     {
                                                                         Name        = "Posit Rfq",
                                                                         Acronym     = "RFQ",
                                                                         MIC         = "XRFQ",
                                                                         Polity      = "Ireland",
                                                                         TimeZone    = TimeSpan.Zero,
                                                                         MarketOpen  = TimeSpan.Zero,
                                                                         MarketClose = TimeSpan.Zero,
                                                                         CurrencyId  = Guid.Parse("6842a5fa-eee4-4438-bcff-5217b6ac6ace")
                                                                     };

        public static readonly StockExchangeResponse Response = new()
                                                                {
                                                                    Id          = Seeder.StockExchange.Nasdaq.Id,
                                                                    Name        = CreateRequest.Name,
                                                                    Acronym     = CreateRequest.Acronym,
                                                                    MIC         = CreateRequest.MIC,
                                                                    Polity      = CreateRequest.Polity,
                                                                    Currency    = null!,
                                                                    TimeZone    = TimeSpan.Zero,
                                                                    MarketOpen  = TimeSpan.Zero,
                                                                    MarketClose = TimeSpan.Zero,
                                                                    CreatedAt   = DateTime.UtcNow,
                                                                    ModifiedAt  = DateTime.UtcNow
                                                                };
    }
}
