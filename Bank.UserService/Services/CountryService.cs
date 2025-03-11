using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ICountryService
{
    Task<Result<Page<CountryResponse>>> FindAll(CountryFilterQuery currencyFilterQuery, Pageable pageable);

    Task<Result<CountryResponse>> FindById(Guid id);
}

public class CountryService(ICountryRepository countryRepository) : ICountryService
{
    private readonly ICountryRepository m_CountryRepository = countryRepository;

    public async Task<Result<Page<CountryResponse>>> FindAll(CountryFilterQuery countryFilterQuery, Pageable pageable)
    {
        var page = await m_CountryRepository.FindAll(countryFilterQuery, pageable);

        var countryResponses = page.Items.Select(country => country.ToResponse())
                                   .ToList();

        return Result.Ok(new Page<CountryResponse>(countryResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<CountryResponse>> FindById(Guid id)
    {
        var country = await m_CountryRepository.FindById(id);

        if (country == null)
            return Result.NotFound<CountryResponse>($"No Country found with Id: {id}");

        return Result.Ok(country.ToResponse());
    }
}
