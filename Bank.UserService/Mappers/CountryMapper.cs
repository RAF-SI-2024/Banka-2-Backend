using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class CountryMapper
{
    public static CountryResponse ToResponse(this Country country)
    {
        return new CountryResponse
               {
                   Id         = country.Id,
                   Name       = country.Name,
                   Currency   = country.Currency?.ToSimpleResponse(),
                   CreatedAt  = country.CreatedAt,
                   ModifiedAt = country.ModifiedAt
               };
    }

    public static CountrySimpleResponse ToSimpleResponse(this Country country)
    {
        return new CountrySimpleResponse
               {
                   Id         = country.Id,
                   Name       = country.Name,
                   CreatedAt  = country.CreatedAt,
                   ModifiedAt = country.ModifiedAt
               };
    }
}
