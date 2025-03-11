using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface ICountryRepository
{
    Task<Page<Country>> FindAll(CountryFilterQuery countryFilterQuery, Pageable pageable);

    Task<Country?> FindById(Guid id);

    Task<Country> Add(Country country);

    Task<Country> Update(Country oldCountry, Country country);
}

public class CountryRepository(ApplicationContext context) : ICountryRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Country>> FindAll(CountryFilterQuery countryFilterQuery, Pageable pageable)
    {
        var countryQuery = m_Context.Countries.Include(c => c.Currency)
                                    .AsQueryable();

        if (!string.IsNullOrEmpty(countryFilterQuery.Name))
            countryQuery = countryQuery.Where(country => EF.Functions.ILike(country.Name, $"%{countryFilterQuery.Name}%"));

        if (!string.IsNullOrEmpty(countryFilterQuery.CurrencyName))
            countryQuery = countryQuery.Include(country => country.Currency)
                                       .Where(country => country.Currency != null && EF.Functions.ILike(country.Currency.Name, $"%{countryFilterQuery.CurrencyName}%"));

        if (!string.IsNullOrEmpty(countryFilterQuery.CurrencyCode))
            countryQuery = countryQuery.Include(country => country.Currency)
                                       .Where(country => country.Currency != null && EF.Functions.ILike(country.Currency.Code, $"%{countryFilterQuery.CurrencyCode}%"));

        int totalElements = await countryQuery.CountAsync();

        var countries = await countryQuery.Skip((pageable.Page - 1) * pageable.Size)
                                          .Take(pageable.Size)
                                          .ToListAsync();

        return new Page<Country>(countries, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Country?> FindById(Guid id)
    {
        return await m_Context.Countries.Include(c => c.Currency)
                              .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Country> Add(Country country)
    {
        var addedCountry = await m_Context.Countries.AddAsync(country);

        await m_Context.SaveChangesAsync();

        return addedCountry.Entity;
    }

    public async Task<Country> Update(Country oldCountry, Country country)
    {
        m_Context.Countries.Entry(oldCountry)
                 .State = EntityState.Detached;

        var updatedCountry = m_Context.Countries.Update(country);

        await m_Context.SaveChangesAsync();

        return updatedCountry.Entity;
    }
}
