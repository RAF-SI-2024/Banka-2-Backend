using System.Linq.Expressions;

using Bank.Application.Queries;
using Bank.Database.Core;
using Bank.UserService.Database;
using Bank.UserService.Models;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface IExchangeRepository
{
    public Task<List<Exchange>> FindAll(ExchangeFilterQuery exchangeFilterQuery);

    public Task<List<Exchange>> FindAllLatest();

    public Task<Exchange?> FindById(Guid id);

    public Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Guid firstCurrencyId, Guid secondCurrencyId);

    public Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Currency firstCurrency, Currency secondCurrency);

    public Task<Exchange> Add(Exchange exchange);

    public Task<bool> AddRange(IEnumerable<Exchange> exchanges);

    public Task<Exchange> Update(Exchange exchange);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class ExchangeRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : IExchangeRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<List<Exchange>> FindAll(ExchangeFilterQuery exchangeFilterQuery)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var exchangeQueue = context.Exchanges.IncludeAll()
                                   .AsQueryable();

        if (!string.IsNullOrEmpty(exchangeFilterQuery.CurrencyCode))
            exchangeQueue = exchangeQueue.Where(exchange => EF.Functions.ILike(exchange.CurrencyFrom!.Code.ToLower(), $"%{exchangeFilterQuery.CurrencyCode.ToLower()}%") ||
                                                            EF.Functions.ILike(exchange.CurrencyTo!.Code.ToLower(),   $"%{exchangeFilterQuery.CurrencyCode.ToLower()}%"));

        if (exchangeFilterQuery.CurrencyId != Guid.Empty)
            exchangeQueue = exchangeQueue.Where(exchange => exchange.CurrencyFromId == exchangeFilterQuery.CurrencyId || exchange.CurrencyToId == exchangeFilterQuery.CurrencyId);

        if (exchangeFilterQuery.Date == DateOnly.MinValue)
            exchangeFilterQuery.Date = DateOnly.FromDateTime(DateTime.UtcNow);

        exchangeQueue = exchangeQueue.Where(exchange => exchangeFilterQuery.Date                  <= DateOnly.FromDateTime(exchange.CreatedAt) &&
                                                        DateOnly.FromDateTime(exchange.CreatedAt) < exchangeFilterQuery.Date.AddDays(1));

        return await exchangeQueue.ToListAsync();
    }

    public async Task<List<Exchange>> FindAllLatest()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Exchanges.IncludeAll()
                            .GroupBy(exchange => new { exchange.CurrencyFromId, exchange.CurrencyToId })
                            .Select(group => group.OrderByDescending(exchange => exchange.CreatedAt)
                                                  .First())
                            .ToListAsync();
    }

    public async Task<Exchange?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await FindById(id, context);
    }

    public async Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Guid firstCurrencyId, Guid secondCurrencyId)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await FindByCurrencyFromAndCurrencyTo(firstCurrencyId, secondCurrencyId, context);
    }

    public async Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Currency firstCurrency, Currency secondCurrency)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await FindByCurrencyFromAndCurrencyTo(firstCurrency.Id, secondCurrency.Id, context);
    }

    public async Task<Exchange> Add(Exchange exchange)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addExchange = await context.Exchanges.AddAsync(exchange);

        await context.SaveChangesAsync();

        return addExchange.Entity;
    }

    public async Task<bool> AddRange(IEnumerable<Exchange> exchanges)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(exchanges, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<Exchange> Update(Exchange exchange)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.Exchanges.Where(dbExchange => dbExchange.Id == exchange.Id)
                     .ExecuteUpdateAsync(setter => setter.SetProperty(dbExchange => dbExchange.Commission, exchange.Commission));

        return exchange;
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Exchanges.AnyAsync(exchange => exchange.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Exchanges.AnyAsync() is not true;
    }

    #region Static Repository Calls

    private static async Task<Exchange?> FindById(Guid id, ApplicationContext context)
    {
        return await context.Exchanges.IncludeAll()
                            .FirstOrDefaultAsync(x => x.Id == id);
    }

    private static async Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Guid firstCurrencyId, Guid secondCurrencyId, ApplicationContext context)
    {
        return await context.Exchanges.IncludeAll()
                            .OrderByDescending(exchange => exchange.CreatedAt)
                            .FirstOrDefaultAsync(exchange => (exchange.CurrencyFromId == firstCurrencyId  && exchange.CurrencyToId == secondCurrencyId) ||
                                                             (exchange.CurrencyFromId == secondCurrencyId && exchange.CurrencyToId == firstCurrencyId));
    }

    #endregion
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<Exchange, object?> IncludeAll(this DbSet<Exchange> set)
    {
        return set.Include(exchange => exchange.CurrencyFrom)
                  .ThenIncludeAll(exchange => exchange.CurrencyFrom)
                  .Include(exchange => exchange.CurrencyTo)
                  .ThenIncludeAll(exchange => exchange.CurrencyTo);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, Exchange?> value,
                                                                                 Expression<Func<TEntity, Exchange?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Exchange.CurrencyFrom)))
            query = query.Include(navigationExpression)
                         .ThenInclude(exchange => exchange!.CurrencyFrom);

        if (!excludeProperties.Contains(nameof(Exchange.CurrencyTo)))
            query = query.Include(navigationExpression)
                         .ThenInclude(exchange => exchange!.CurrencyTo);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<Exchange>> value,
                                                                                 Expression<Func<TEntity, List<Exchange>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Exchange.CurrencyFrom)))
            query = query.Include(navigationExpression)
                         .ThenInclude(exchange => exchange.CurrencyFrom);

        if (!excludeProperties.Contains(nameof(Exchange.CurrencyTo)))
            query = query.Include(navigationExpression)
                         .ThenInclude(exchange => exchange.CurrencyTo);

        return query;
    }
}
