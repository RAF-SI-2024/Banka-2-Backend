using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class ForexPair
    {
        public static readonly ForexPairResponse DefaultResponse = new()
                                                                   {
                                                                       Id                           = Constant.Id,
                                                                       Liquidity                    = Constant.Liquidity,
                                                                       BaseCurrency                 = Currency.DefaultSimpleResponse,
                                                                       QuoteCurrency                = Currency.DefaultSimpleResponse,
                                                                       ExchangeRate                 = Constant.Rate,
                                                                       MaintenanceDecimal           = Constant.MaintenanceDecimal,
                                                                       Name                         = Constant.ForexPairName,
                                                                       Ticker                       = Constant.Ticker,
                                                                       HighPrice                    = Constant.HighPrice,
                                                                       LowPrice                     = Constant.LowPrice,
                                                                       AskPrice                     = Constant.AskPrice,
                                                                       BidPrice                     = Constant.BidPrice,
                                                                       PriceChangeInInterval        = Constant.PriceChangeInInterval,
                                                                       PriceChangePercentInInterval = Constant.PriceChangePercentInInterval,
                                                                       CreatedAt                    = Constant.CreatedAt,
                                                                       ModifiedAt                   = Constant.ModifiedAt,
                                                                       StockExchange                = StockExchange.DefaultResponse,
                                                                       Quotes =
                                                                       [
                                                                           Quote.DefaultSimpleResponse
                                                                       ],
                                                                       AskSize       = 24,
                                                                       BidSize       = 12,
                                                                       ContractCount = Constant.ContractCount,
                                                                   };

        public static readonly ForexPairSimpleResponse DefaultSimpleResponse = new()
                                                                               {
                                                                                   Id                           = Constant.Id,
                                                                                   Liquidity                    = Constant.Liquidity,
                                                                                   BaseCurrency                 = Currency.DefaultSimpleResponse,
                                                                                   QuoteCurrency                = Currency.DefaultSimpleResponse,
                                                                                   ExchangeRate                 = Constant.Rate,
                                                                                   MaintenanceDecimal           = Constant.MaintenanceDecimal,
                                                                                   Name                         = Constant.ForexPairName,
                                                                                   Ticker                       = Constant.Ticker,
                                                                                   Price                        = Constant.Price,
                                                                                   HighPrice                    = Constant.HighPrice,
                                                                                   LowPrice                     = Constant.LowPrice,
                                                                                   AskPrice                     = Constant.AskPrice,
                                                                                   BidPrice                     = Constant.BidPrice,
                                                                                   PriceChangeInInterval        = Constant.PriceChangeInInterval,
                                                                                   PriceChangePercentInInterval = Constant.PriceChangePercentInInterval,
                                                                                   CreatedAt                    = Constant.CreatedAt,
                                                                                   ModifiedAt                   = Constant.ModifiedAt,
                                                                                   AskSize                      = 45,
                                                                                   BidSize                      = 40,
                                                                                   ContractCount                = Constant.ContractCount
                                                                               };

        public static readonly ForexPairDailyResponse DefaultDailyResponse = new()
                                                                             {
                                                                                 Id                           = Constant.Id,
                                                                                 Liquidity                    = Constant.Liquidity,
                                                                                 BaseCurrency                 = Currency.DefaultSimpleResponse,
                                                                                 QuoteCurrency                = Currency.DefaultSimpleResponse,
                                                                                 ExchangeRate                 = Constant.Rate,
                                                                                 MaintenanceDecimal           = Constant.MaintenanceDecimal,
                                                                                 Name                         = Constant.ForexPairName,
                                                                                 Ticker                       = Constant.Ticker,
                                                                                 HighPrice                    = Constant.HighPrice,
                                                                                 LowPrice                     = Constant.LowPrice,
                                                                                 OpeningPrice                 = Constant.OpeningPrice,
                                                                                 ClosePrice                   = Constant.ClosePrice,
                                                                                 PriceChangeInInterval        = Constant.PriceChangeInInterval,
                                                                                 PriceChangePercentInInterval = Constant.PriceChangePercentInInterval,
                                                                                 CreatedAt                    = Constant.CreatedAt,
                                                                                 ModifiedAt                   = Constant.ModifiedAt,
                                                                                 StockExchange                = StockExchange.DefaultResponse,
                                                                                 Quotes                       = [Quote.DefaultDailySimpleResponse]
                                                                             };
    }
}
