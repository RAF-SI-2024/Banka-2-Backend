using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class ExchangeMapper
{
    public static ExchangeResponse ToResponse(this Exchange exchange)
    {
        return new ExchangeResponse
               {
                   Id           = exchange.Id,
                   CurrencyFrom = exchange.CurrencyFrom.ToSimpleResponse(),
                   CurrencyTo   = exchange.CurrencyTo.ToSimpleResponse(),
                   Commission   = exchange.Commission,
                   Rate         = exchange.Rate,
                   InverseRate  = exchange.InverseRate,
                   CreatedAt    = exchange.CreatedAt,
                   ModifiedAt   = exchange.ModifiedAt
               };
    }

    public static Exchange Inverse(this Exchange exchange)
    {
        return new Exchange
               {
                   Id             = exchange.Id,
                   CurrencyFromId = exchange.CurrencyToId,
                   CurrencyFrom   = exchange.CurrencyTo,
                   CurrencyToId   = exchange.CurrencyFromId,
                   CurrencyTo     = exchange.CurrencyFrom,
                   Commission     = exchange.Commission,
                   Rate           = exchange.InverseRate,
                   CreatedAt      = exchange.CreatedAt,
                   ModifiedAt     = exchange.ModifiedAt
               };
    }

    public static Exchange ToExchange(this ExchangeUpdateRequest exchangeUpdateRequest, Exchange oldExchange)
    {
        return new Exchange
               {
                   Id             = oldExchange.Id,
                   CurrencyFromId = oldExchange.CurrencyFromId,
                   CurrencyFrom   = oldExchange.CurrencyFrom,
                   CurrencyToId   = oldExchange.CurrencyToId,
                   CurrencyTo     = oldExchange.CurrencyTo,
                   Commission     = exchangeUpdateRequest.Commission,
                   Rate           = oldExchange.Rate,
                   CreatedAt      = oldExchange.CreatedAt,
                   ModifiedAt     = DateTime.UtcNow
               };
    }

    public static Exchange ToExchange(this ExchangeFetchResponse exchangeFetchResponse, Currency currencyFrom, Currency currencyTo, decimal commission)
    {
        return new Exchange
               {
                   Id             = Guid.NewGuid(),
                   CurrencyFrom   = currencyFrom,
                   CurrencyFromId = currencyFrom.Id,
                   CurrencyTo     = currencyTo,
                   CurrencyToId   = currencyTo.Id,
                   Commission     = commission,
                   Rate           = exchangeFetchResponse.Rate,
                   CreatedAt      = DateTime.UtcNow,
                   ModifiedAt     = DateTime.UtcNow
               };
    }
}
