using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.ExchangeService.Models;

namespace Bank.ExchangeService.Mappers;

public static class ForexPairMapper
{
    public static ForexPairResponse ToResponse(this ForexPair         forexPair, CurrencySimpleResponse currencyResponse, CurrencySimpleResponse currencyBaseResponse,
                                               CurrencySimpleResponse currencyQuoteResponse)
    {
        return new ForexPairResponse
               {
                   Id                           = forexPair.Id,
                   Liquidity                    = forexPair.Liquidity,
                   ExchangeRate                 = forexPair.ExchangeRate,
                   BaseCurrency                 = currencyBaseResponse,
                   QuoteCurrency                = currencyQuoteResponse,
                   MaintenanceDecimal           = forexPair.MaintenanceDecimal,
                   Name                         = forexPair.Name,
                   Ticker                       = forexPair.Ticker,
                   StockExchange                = forexPair.StockExchange!.ToResponse(currencyResponse),
                   HighPrice                    = forexPair.HighPrice,
                   LowPrice                     = forexPair.LowPrice,
                   PriceChangeInInterval        = forexPair.PriceChange,
                   PriceChangePercentInInterval = forexPair.PriceChangePercent,
                   AskPrice                     = forexPair.AskPrice,
                   BidPrice                     = forexPair.BidPrice,
                   CreatedAt                    = forexPair.CreatedAt,
                   ModifiedAt                   = forexPair.ModifiedAt,
                   Quotes = forexPair.Quotes.Select(quote => quote.ToSimpleResponse())
                                     .ToList(),
                   AskSize       = forexPair.AskSize,
                   BidSize       = forexPair.BidSize,
                   ContractCount = forexPair.ContractCount
               };
    }

    public static ForexPairDailyResponse ToCandleResponse(this ForexPair         forexPair, CurrencySimpleResponse currencyResponse, CurrencySimpleResponse currencyBaseResponse,
                                                          CurrencySimpleResponse currencyQuoteResponse)
    {
        return new ForexPairDailyResponse
               {
                   Id                           = forexPair.Id,
                   Liquidity                    = forexPair.Liquidity,
                   ExchangeRate                 = forexPair.ExchangeRate,
                   BaseCurrency                 = currencyBaseResponse,
                   QuoteCurrency                = currencyQuoteResponse,
                   MaintenanceDecimal           = forexPair.MaintenanceDecimal,
                   Name                         = forexPair.Name,
                   Ticker                       = forexPair.Ticker,
                   StockExchange                = forexPair.StockExchange!.ToResponse(currencyResponse),
                   HighPrice                    = forexPair.HighPrice,
                   LowPrice                     = forexPair.LowPrice,
                   PriceChangeInInterval        = forexPair.PriceChange,
                   PriceChangePercentInInterval = forexPair.PriceChangePercent,
                   CreatedAt                    = forexPair.CreatedAt,
                   ModifiedAt                   = forexPair.ModifiedAt,
                   OpeningPrice                 = forexPair.OpeningPrice,
                   ClosePrice                   = forexPair.ClosePrice,
                   Quotes = forexPair.DailyQuotes.Select(quote => quote.ToDailySimpleResponse())
                                     .ToList()
               };
    }

    public static ForexPairSimpleResponse ToSimpleResponse(this ForexPair forexPair, CurrencySimpleResponse currencyBaseResponse, CurrencySimpleResponse currencyQuoteResponse)
    {
        return new ForexPairSimpleResponse
               {
                   Id                           = forexPair.Id,
                   Liquidity                    = forexPair.Liquidity,
                   ExchangeRate                 = forexPair.ExchangeRate,
                   BaseCurrency                 = currencyBaseResponse,
                   QuoteCurrency                = currencyQuoteResponse,
                   MaintenanceDecimal           = forexPair.MaintenanceDecimal,
                   Name                         = forexPair.Name,
                   Ticker                       = forexPair.Ticker,
                   HighPrice                    = forexPair.HighPrice,
                   LowPrice                     = forexPair.LowPrice,
                   AskPrice                     = forexPair.AskPrice,
                   BidPrice                     = forexPair.BidPrice,
                   PriceChangeInInterval        = forexPair.PriceChange,
                   PriceChangePercentInInterval = forexPair.PriceChangePercent,
                   Price                        = forexPair.AskPrice,
                   CreatedAt                    = forexPair.CreatedAt,
                   ModifiedAt                   = forexPair.ModifiedAt,
                   AskSize                      = forexPair.AskSize,
                   BidSize                      = forexPair.BidSize,
                   ContractCount                = forexPair.ContractCount
               };
    }

    public static ForexPair ToForexPair(this FetchForexPairLatestResponse fetchForexPair, CurrencySimpleResponse currencyFrom, CurrencySimpleResponse currencyTo,
                                        Liquidity                         liquidity,      Guid                   stockExchangeId)
    {
        return new ForexPair
               {
                   Id              = Guid.NewGuid(),
                   BaseCurrencyId  = currencyFrom.Id,
                   QuoteCurrencyId = currencyTo.Id,
                   ExchangeRate    = fetchForexPair.ExchangeRate,
                   Name            = $"{currencyFrom.Code}/{currencyTo.Code}",
                   Ticker          = $"{currencyFrom.Code}{currencyTo.Code}",
                   Liquidity       = liquidity,
                   StockExchangeId = stockExchangeId
               };
    }
}
