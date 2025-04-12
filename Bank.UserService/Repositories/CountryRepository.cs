using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ICountryRepository
{
    Task<Page<Country>> FindAll(CountryFilterQuery countryFilterQuery, Pageable pageable);

    Task<Country?> FindById(Guid id);
}

public class CountryRepository(ApplicationContext context) : ICountryRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Country>> FindAll(CountryFilterQuery countryFilterQuery, Pageable pageable)
    {
        var countryQuery = m_Context.Countries.IncludeAll()
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
        return await m_Context.Countries.IncludeAll()
                              .FirstOrDefaultAsync(x => x.Id == id);
    }
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<Country, object?> IncludeAll(this DbSet<Country> set)
    {
        return set.Include(country => country.Currency)
                  .ThenIncludeAll(country => country.Currency, nameof(Currency.Countries));
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, Country?> value,
                                                                                 Expression<Func<TEntity, Country?>>          navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Country.Currency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(country => country!.Currency);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<Country>> value,
                                                                                 Expression<Func<TEntity, List<Country>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Country.Currency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(country => country.Currency);

        return query;
    }
}
