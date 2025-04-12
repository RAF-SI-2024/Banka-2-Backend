using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class CurrencyMapper
{
    public static CurrencyResponse ToResponse(this Currency currency)
    {
        return new CurrencyResponse
               {
                   Id          = currency.Id,
                   Name        = currency.Name,
                   Code        = currency.Code,
                   Symbol      = currency.Symbol,
                   Countries   = MapToCountrySimpleResponses(currency.Countries),
                   Description = currency.Description,
                   Status      = currency.Status,
                   CreatedAt   = currency.CreatedAt,
                   ModifiedAt  = currency.ModifiedAt
               };
    }

    public static CurrencySimpleResponse ToSimpleResponse(this Currency currency)
    {
        return new CurrencySimpleResponse
               {
                   Id          = currency.Id,
                   Name        = currency.Name,
                   Code        = currency.Code,
                   Symbol      = currency.Symbol,
                   Description = currency.Description,
                   Status      = currency.Status,
                   CreatedAt   = currency.CreatedAt,
                   ModifiedAt  = currency.ModifiedAt
               };
    }

    private static List<CountrySimpleResponse> MapToCountrySimpleResponses(List<Country> countries) =>
    countries.Select(country => country.ToSimpleResponse())
             .ToList();
}
