using System.Linq.Expressions;

using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface IExchangeRepository
{
    public Task<List<Exchange>> FindAll(ExchangeFilterQuery exchangeFilterQuery);

    public Task<Exchange?> FindById(Guid id);

    public Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Guid firstCurrencyId, Guid secondCurrencyId);

    public Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Currency firstCurrency, Currency secondCurrency);

    public Task<Exchange> Add(Exchange exchange);

    public Task<Exchange> Update(Exchange exchange);
}

public class ExchangeRepository(ApplicationContext context, IDbContextFactory<ApplicationContext> contextFactory) : IExchangeRepository
{
    private readonly ApplicationContext m_Context = context;

    private readonly IDbContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    private Task<ApplicationContext> CreateContext => m_ContextFactory.CreateDbContextAsync();

    public async Task<List<Exchange>> FindAll(ExchangeFilterQuery exchangeFilterQuery)
    {
        var exchangeQueue = m_Context.Exchanges.IncludeAll()
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

    public async Task<Exchange?> FindById(Guid id)
    {
        await using var context = await CreateContext;

        return await FindById(id, context);
    }

    public async Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Guid firstCurrencyId, Guid secondCurrencyId)
    {
        await using var context = await CreateContext;

        return await FindByCurrencyFromAndCurrencyTo(firstCurrencyId, secondCurrencyId, context);
    }

    public async Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Currency firstCurrency, Currency secondCurrency)
    {
        await using var context = await CreateContext;

        return await FindByCurrencyFromAndCurrencyTo(firstCurrency.Id, secondCurrency.Id, context);
    }

    public async Task<Exchange> Add(Exchange exchange)
    {
        var addExchange = await m_Context.Exchanges.AddAsync(exchange);

        await m_Context.SaveChangesAsync();

        return addExchange.Entity;
    }

    public async Task<Exchange> Update(Exchange exchange)
    {
        await m_Context.Exchanges.Where(dbExchange => dbExchange.Id == exchange.Id)
                       .ExecuteUpdateAsync(setter => setter.SetProperty(dbExchange => dbExchange.Commission, exchange.Commission));

        return exchange;
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
                                                                                 Expression<Func<TEntity, Exchange?>>          navigationExpression, params string[] excludeProperties)
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
