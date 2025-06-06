using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.ExchangeService.Models;

namespace Bank.ExchangeService.Mappers;

public static class OptionMapper
{
    public static OptionResponse ToResponse(this Option option, CurrencySimpleResponse currencySimpleResponse)
    {
        return new OptionResponse
               {
                   Id                           = option.Id,
                   StrikePrice                  = option.StrikePrice,
                   ImpliedVolatility            = option.ImpliedVolatility,
                   SettlementDate               = option.SettlementDate,
                   Name                         = option.Name,
                   Ticker                       = option.Ticker,
                   StockExchange                = option.StockExchange!.ToResponse(currencySimpleResponse),
                   OptionType                   = option.OptionType,
                   HighPrice                    = option.HighPrice,
                   LowPrice                     = option.LowPrice,
                   Volume                       = option.Volume,
                   PriceChangeInInterval        = option.PriceChange,
                   PriceChangePercentInInterval = option.PriceChangePercent,
                   AskPrice                     = option.AskPrice,
                   BidPrice                     = option.BidPrice,
                   CreatedAt                    = option.CreatedAt,
                   ModifiedAt                   = option.ModifiedAt,
                   AskSize                      = option.AskSize,
                   BidSize                      = option.BidSize,
                   Quotes = option.Quotes.Select(quote => quote.ToChartSimpleResponse())
                                  .ToList(),
                   ContractCount = option.ContractCount,
               };
    }

    public static OptionDailyResponse ToCandleResponse(this Option option, CurrencySimpleResponse currencySimpleResponse)
    {
        return new OptionDailyResponse
               {
                   Id                           = option.Id,
                   StrikePrice                  = option.StrikePrice,
                   ImpliedVolatility            = option.ImpliedVolatility,
                   SettlementDate               = option.SettlementDate,
                   Name                         = option.Name,
                   Ticker                       = option.Ticker,
                   StockExchange                = option.StockExchange!.ToResponse(currencySimpleResponse),
                   OptionType                   = option.OptionType,
                   HighPrice                    = option.HighPrice,
                   LowPrice                     = option.LowPrice,
                   Volume                       = option.Volume,
                   PriceChangeInInterval        = option.PriceChange,
                   PriceChangePercentInInterval = option.PriceChangePercent,
                   CreatedAt                    = option.CreatedAt,
                   ModifiedAt                   = option.ModifiedAt,
                   OpeningPrice                 = option.OpeningPrice,
                   ClosePrice                   = option.ClosePrice,
                   Quotes = option.DailyQuotes.Select(quote => quote.ToDailySimpleResponse())
                                  .ToList(),
               };
    }

    public static OptionSimpleResponse ToSimpleResponse(this Option option)
    {
        return new OptionSimpleResponse
               {
                   Id                           = option.Id,
                   StrikePrice                  = option.StrikePrice,
                   ImpliedVolatility            = option.ImpliedVolatility,
                   SettlementDate               = option.SettlementDate,
                   Name                         = option.Name,
                   Ticker                       = option.Ticker,
                   OptionType                   = option.OptionType,
                   HighPrice                    = option.HighPrice,
                   LowPrice                     = option.LowPrice,
                   Volume                       = option.Volume,
                   PriceChange                  = option.PriceChange,
                   AskPrice                     = option.AskPrice,
                   BidPrice                     = option.BidPrice,
                   CreatedAt                    = option.CreatedAt,
                   ModifiedAt                   = option.ModifiedAt,
                   PriceChangeInInterval        = option.PriceChange,
                   PriceChangePercentInInterval = option.PriceChangePercent,
                   AskSize                      = option.AskSize,
                   BidSize                      = option.BidSize,
                   ContractCount                = option.ContractCount,
               };
    }

    public static Option ToOption(this FetchOptionOneResponse optionResponse, Stock stock, string optionTicker, DateOnly settlementDate, decimal strikePrice, OptionType optionType)
    {
        return new Option
               {
                   Id                = Guid.NewGuid(),
                   OptionType        = optionType,
                   StrikePrice       = strikePrice,
                   ImpliedVolatility = optionResponse.ImpliedVolatility,
                   SettlementDate    = settlementDate,
                   Name              = stock.Name,
                   Ticker            = optionTicker,
                   StockExchangeId   = stock.StockExchangeId
               };
    }
}
