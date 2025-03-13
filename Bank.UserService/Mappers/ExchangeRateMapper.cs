using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class ExchangeRateMapper
{
    public static ExchangeRateResponse ToResponse(this ExchangeRate exchangeRate)
    {
        return new ExchangeRateResponse
               {
                   Id           = exchangeRate.Id,
                   CurrencyFrom = exchangeRate.CurrencyFrom.ToSimpleResponse(),
                   CurrencyTo   = exchangeRate.CurrencyTo.ToSimpleResponse(),
                   Commission   = exchangeRate.Commission,
                   Rate         = exchangeRate.Rate,
                   InverseRate  = exchangeRate.InverseRate,
                   CreatedAt    = exchangeRate.CreatedAt,
                   ModifiedAt   = exchangeRate.ModifiedAt
               };
    }

    public static ExchangeRate Inverse(this ExchangeRate exchangeRate)
    {
        return new ExchangeRate
               {
                   Id             = exchangeRate.Id,
                   CurrencyFromId = exchangeRate.CurrencyToId,
                   CurrencyFrom   = exchangeRate.CurrencyTo,
                   CurrencyToId   = exchangeRate.CurrencyFromId,
                   CurrencyTo     = exchangeRate.CurrencyFrom,
                   Commission     = exchangeRate.Commission,
                   Rate           = exchangeRate.InverseRate,
                   CreatedAt      = exchangeRate.CreatedAt,
                   ModifiedAt     = exchangeRate.ModifiedAt
               };
    }

    public static ExchangeRate ToExchangeRate(this ExchangeRateUpdateRequest exchangeRateUpdateRequest, ExchangeRate oldExchangeRate)
    {
        return new ExchangeRate
               {
                   Id             = oldExchangeRate.Id,
                   CurrencyFromId = oldExchangeRate.CurrencyFromId,
                   CurrencyFrom   = oldExchangeRate.CurrencyFrom,
                   CurrencyToId   = oldExchangeRate.CurrencyToId,
                   CurrencyTo     = oldExchangeRate.CurrencyTo,
                   Commission     = exchangeRateUpdateRequest.Commission,
                   Rate           = oldExchangeRate.Rate,
                   CreatedAt      = oldExchangeRate.CreatedAt,
                   ModifiedAt     = DateTime.UtcNow
               };
    }

    public static ExchangeRate ToExchangeRate(this ExchangeRateFetchResponse exchangeRateFetchResponse, Currency currencyFrom, Currency currencyTo)
    {
        return new ExchangeRate
               {
                   Id             = Guid.NewGuid(),
                   CurrencyFrom   = currencyFrom,
                   CurrencyFromId = currencyFrom.Id,
                   CurrencyTo     = currencyTo,
                   CurrencyToId   = currencyTo.Id,
                   Commission     = 0.005m,
                   Rate           = exchangeRateFetchResponse.Rate,
                   CreatedAt      = DateTime.UtcNow,
                   ModifiedAt     = DateTime.UtcNow
               };
    }

    public static ExchangeRate ToExchangeRate(this ExchangeRateFetchResponse exchangeRateFetchResponse, ExchangeRate oldExchangeRate)
    {
        return new ExchangeRate
               {
                   Id             = oldExchangeRate.Id,
                   CurrencyFromId = oldExchangeRate.CurrencyFromId,
                   CurrencyFrom   = oldExchangeRate.CurrencyFrom,
                   CurrencyToId   = oldExchangeRate.CurrencyToId,
                   CurrencyTo     = oldExchangeRate.CurrencyTo,
                   Commission     = oldExchangeRate.Commission,
                   Rate           = exchangeRateFetchResponse.Rate,
                   CreatedAt      = oldExchangeRate.CreatedAt,
                   ModifiedAt     = DateTime.UtcNow
               };
    }
}
