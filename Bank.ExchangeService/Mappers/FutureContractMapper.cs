using Bank.Application.Responses;
using Bank.ExchangeService.Models;

namespace Bank.ExchangeService.Mappers;

public static class FutureContractMapper
{
    public static FutureContractResponse ToResponse(this FutureContract futureContract, CurrencySimpleResponse currencyResponse)
    {
        return new FutureContractResponse
               {
                   Id                           = futureContract.Id,
                   ContractSize                 = futureContract.ContractSize,
                   ContractUnit                 = futureContract.ContractUnit,
                   SettlementDate               = futureContract.SettlementDate,
                   Name                         = futureContract.Name,
                   Ticker                       = futureContract.Ticker,
                   StockExchange                = futureContract.StockExchange!.ToResponse(currencyResponse),
                   HighPrice                    = futureContract.HighPrice,
                   LowPrice                     = futureContract.LowPrice,
                   Volume                       = futureContract.Volume,
                   PriceChangeInInterval        = futureContract.PriceChange,
                   PriceChangePercentInInterval = futureContract.PriceChangePercent,
                   AskPrice                     = futureContract.AskPrice,
                   BidPrice                     = futureContract.BidPrice,
                   AskSize                      = futureContract.AskSize,
                   BidSize                      = futureContract.BidSize,
                   CreatedAt                    = futureContract.CreatedAt,
                   ModifiedAt                   = futureContract.ModifiedAt,
                   Quotes = futureContract.Quotes.Select(quote => quote.ToSimpleResponse())
                                          .ToList(),
                   ContractCount = 0,
               };
    }

    public static FutureContractSimpleResponse ToSimpleResponse(this FutureContract futureContract)
    {
        return new FutureContractSimpleResponse
               {
                   Id                           = futureContract.Id,
                   ContractSize                 = futureContract.ContractSize,
                   ContractUnit                 = futureContract.ContractUnit,
                   SettlementDate               = futureContract.SettlementDate,
                   Name                         = futureContract.Name,
                   Ticker                       = futureContract.Ticker,
                   HighPrice                    = futureContract.HighPrice,
                   LowPrice                     = futureContract.LowPrice,
                   Volume                       = futureContract.Volume,
                   AskPrice                     = futureContract.AskPrice,
                   BidPrice                     = futureContract.BidPrice,
                   AskSize                      = futureContract.AskSize,
                   BidSize                      = futureContract.BidSize,
                   CreatedAt                    = futureContract.CreatedAt,
                   ModifiedAt                   = futureContract.ModifiedAt,
                   PriceChangeInInterval        = futureContract.PriceChange,
                   PriceChangePercentInInterval = futureContract.PriceChangePercent,
                   ContractCount                = 0
               };
    }

    public static FutureContractDailyResponse ToDailyResponse(this FutureContract futureContract, CurrencySimpleResponse currencyResponse)
    {
        return new FutureContractDailyResponse
               {
                   Id                           = futureContract.Id,
                   ContractSize                 = futureContract.ContractSize,
                   ContractUnit                 = futureContract.ContractUnit,
                   SettlementDate               = futureContract.SettlementDate,
                   Name                         = futureContract.Name,
                   Ticker                       = futureContract.Ticker,
                   StockExchange                = futureContract.StockExchange!.ToResponse(currencyResponse),
                   HighPrice                    = futureContract.HighPrice,
                   LowPrice                     = futureContract.LowPrice,
                   Volume                       = futureContract.Volume,
                   PriceChangeInInterval        = futureContract.PriceChange,
                   PriceChangePercentInInterval = futureContract.PriceChangePercent,
                   OpenPrice                    = futureContract.OpeningPrice,
                   ClosePrice                   = futureContract.ClosePrice,
                   CreatedAt                    = futureContract.CreatedAt,
                   ModifiedAt                   = futureContract.ModifiedAt,
                   Quotes = futureContract.DailyQuotes.Select(quote => quote.ToDailySimpleResponse())
                                          .ToList(),
               };
    }
}
