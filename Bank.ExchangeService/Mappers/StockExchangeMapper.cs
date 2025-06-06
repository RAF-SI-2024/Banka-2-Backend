using Bank.Application.Responses;
using Bank.ExchangeService.Model;

namespace Bank.ExchangeService.Mappers;

public static class StockExchangeMapper
{
    public static StockExchangeResponse ToResponse(this StockExchange exchange, CurrencySimpleResponse currency)
    {
        return new StockExchangeResponse
               {
                   Id          = exchange.Id,
                   Name        = exchange.Name,
                   Acronym     = exchange.Acronym,
                   MIC         = exchange.MIC,
                   Polity      = exchange.Polity,
                   TimeZone    = exchange.TimeZone,
                   MarketOpen  = exchange.MarketOpen,
                   MarketClose = exchange.MarketClose,
                   Currency    = currency,
                   CreatedAt   = exchange.CreatedAt,
                   ModifiedAt  = exchange.ModifiedAt
               };
    }

    public static StockExchange ToStockExchange(this ExchangeCreateRequest request)
    {
        return new StockExchange
               {
                   Id          = Guid.NewGuid(),
                   Name        = request.Name,
                   Acronym     = request.Acronym,
                   MIC         = request.MIC,
                   Polity      = request.Polity,
                   CurrencyId  = request.CurrencyId,
                   TimeZone    = request.TimeZone,
                   MarketOpen  = request.MarketOpen,
                   MarketClose = request.MarketClose,
                   CreatedAt   = DateTime.UtcNow,
                   ModifiedAt  = DateTime.UtcNow
               };
    }
}
