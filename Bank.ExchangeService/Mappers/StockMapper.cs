using Bank.Application.Responses;
using Bank.ExchangeService.Models;

namespace Bank.ExchangeService.Mappers;

public static class StockMapper
{
    public static StockResponse ToResponse(this Stock stock, CurrencySimpleResponse currencyResponse)
    {
        return new StockResponse
               {
                   Id            = stock.Id,
                   Name          = stock.Name,
                   Ticker        = stock.Ticker,
                   StockExchange = stock.StockExchange!.ToResponse(currencyResponse),
                   Quotes = stock.Quotes.Select(quote => quote.ToChartSimpleResponse())
                                 .ToList(),
                   HighPrice                    = stock.HighPrice,
                   LowPrice                     = stock.LowPrice,
                   Volume                       = stock.Volume,
                   PriceChangeInInterval        = stock.PriceChange,
                   PriceChangePercentInInterval = stock.PriceChangePercent,
                   CreatedAt                    = stock.CreatedAt,
                   ModifiedAt                   = stock.ModifiedAt,
                   AskPrice                     = stock.AskPrice,
                   BidPrice                     = stock.BidPrice,
                   AskSize                      = stock.AskSize,
                   BidSize                      = stock.BidSize,
                   ContractCount                = stock.ContractCount,
               };
    }

    public static StockSimpleResponse ToSimpleResponse(this Stock stock)
    {
        return new StockSimpleResponse
               {
                   Id                           = stock.Id,
                   Name                         = stock.Name,
                   Ticker                       = stock.Ticker,
                   HighPrice                    = stock.HighPrice,
                   LowPrice                     = stock.LowPrice,
                   Volume                       = stock.Volume,
                   PriceChangeInInterval        = stock.PriceChange,
                   PriceChangePercentInInterval = stock.PriceChangePercent,
                   CreatedAt                    = stock.CreatedAt,
                   ModifiedAt                   = stock.ModifiedAt,
                   AskPrice                     = stock.AskPrice,
                   BidPrice                     = stock.BidPrice,
                   AskSize                      = stock.AskSize,
                   BidSize                      = stock.BidSize,
                   ContractCount                = stock.ContractCount,
               };
    }

    public static StockDailyResponse ToDailyResponse(this Stock stock, CurrencySimpleResponse currencyResponse)
    {
        return new StockDailyResponse
               {
                   Id                           = stock.Id,
                   Name                         = stock.Name,
                   Ticker                       = stock.Ticker,
                   HighPrice                    = stock.HighPrice,
                   LowPrice                     = stock.LowPrice,
                   Volume                       = stock.Volume,
                   PriceChangeInInterval        = stock.PriceChange,
                   PriceChangePercentInInterval = stock.PriceChangePercent,
                   CreatedAt                    = stock.CreatedAt,
                   ModifiedAt                   = stock.ModifiedAt,
                   OpenPrice                    = stock.OpeningPrice,
                   ClosePrice                   = stock.ClosePrice,
                   Quotes = stock.DailyQuotes.Select(quote => quote.ToDailySimpleResponse())
                                 .ToList(),
                   StockExchange = stock.StockExchange!.ToResponse(currencyResponse),
               };
    }

    public static Stock ToStock(this FetchStockResponse stockResponse, Guid stockExchangeId)
    {
        return new Stock
               {
                   Id              = Guid.NewGuid(),
                   Name            = stockResponse.Name,
                   Ticker          = stockResponse.Ticker,
                   StockExchangeId = stockExchangeId,
               };
    }
}
