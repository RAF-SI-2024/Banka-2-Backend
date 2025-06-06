using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Database.Core;
using Bank.UserService.Database;
using Bank.UserService.Models;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ICountryRepository
{
    Task<Page<Country>> FindAll(CountryFilterQuery countryFilterQuery, Pageable pageable);

    Task<Country?> FindById(Guid id);

    Task<bool> AddRange(IEnumerable<Country> countries);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class CountryRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : ICountryRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<Page<Country>> FindAll(CountryFilterQuery countryFilterQuery, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var countryQuery = context.Countries.IncludeAll()
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
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Countries.IncludeAll()
                            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> AddRange(IEnumerable<Country> countries)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(countries, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Countries.AnyAsync(country => country.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Countries.AnyAsync() is not true;
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
                                                                                 Expression<Func<TEntity, Country?>> navigationExpression, params string[] excludeProperties)
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
