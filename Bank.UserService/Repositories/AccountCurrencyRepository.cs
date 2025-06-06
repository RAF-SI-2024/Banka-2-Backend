using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Database.Core;
using Bank.UserService.Database;
using Bank.UserService.Models;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface IAccountCurrencyRepository
{
    Task<Page<AccountCurrency>> FindAll(Pageable pageable);

    Task<AccountCurrency?> FindById(Guid id);

    Task<AccountCurrency> Add(AccountCurrency accountCurrency);

    Task<bool> AddRange(IEnumerable<AccountCurrency> accountCurrencies);

    Task<AccountCurrency> Update(AccountCurrency accountCurrency);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class AccountCurrencyRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : IAccountCurrencyRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<Page<AccountCurrency>> FindAll(Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var accountTypeQuery = context.AccountCurrencies.IncludeAll()
                                      .AsQueryable();

        var accountTypes = await accountTypeQuery.Skip((pageable.Page - 1) * pageable.Size)
                                                 .Take(pageable.Size)
                                                 .ToListAsync();

        var totalElements = await accountTypeQuery.CountAsync();

        return new Page<AccountCurrency>(accountTypes, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<AccountCurrency?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.AccountCurrencies.IncludeAll()
                            .FirstOrDefaultAsync(accountCurrency => accountCurrency.Id == id);
    }

    public async Task<AccountCurrency> Add(AccountCurrency accountCurrency)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addedAccountCurrency = await context.AccountCurrencies.AddAsync(accountCurrency);

        await context.SaveChangesAsync();

        return addedAccountCurrency.Entity;
    }

    public async Task<bool> AddRange(IEnumerable<AccountCurrency> accountCurrencies)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(accountCurrencies, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<AccountCurrency> Update(AccountCurrency accountCurrency)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.AccountCurrencies.Where(dbAccountCurrency => dbAccountCurrency.Id == accountCurrency.Id)
                     .ExecuteUpdateAsync(setters => setters.SetProperty(dbAccountCurrency => dbAccountCurrency.DailyLimit, accountCurrency.DailyLimit)
                                                           .SetProperty(dbAccountCurrency => dbAccountCurrency.MonthlyLimit, accountCurrency.MonthlyLimit)
                                                           .SetProperty(dbAccountCurrency => dbAccountCurrency.ModifiedAt,   accountCurrency.ModifiedAt));

        return accountCurrency;
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.AccountCurrencies.AnyAsync(accountCurrency => accountCurrency.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.AccountCurrencies.AnyAsync() is not true;
    }
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<AccountCurrency, object?> IncludeAll(this DbSet<AccountCurrency> set)
    {
        return set.Include(accountCurrency => accountCurrency.Employee)
                  .ThenIncludeAll(accountCurrency => accountCurrency.Employee)
                  .Include(accountCurrency => accountCurrency.Currency)
                  .ThenIncludeAll(accountCurrency => accountCurrency.Currency)
                  .Include(accountCurrency => accountCurrency.Account)
                  .ThenIncludeAll(accountCurrency => accountCurrency.Account, nameof(Account.AccountCurrencies));
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, AccountCurrency?> value,
                                                                                 Expression<Func<TEntity, AccountCurrency?>>          navigationExpression,
                                                                                 params string[]                                      excludeProperties) where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(AccountCurrency.Employee)))
            query = query.Include(navigationExpression)
                         .ThenInclude(accountCurrency => accountCurrency!.Employee);

        if (!excludeProperties.Contains(nameof(AccountCurrency.Account)))
            query = query.Include(navigationExpression)
                         .ThenInclude(accountCurrency => accountCurrency!.Account);

        if (!excludeProperties.Contains(nameof(AccountCurrency.Currency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(accountCurrency => accountCurrency!.Currency);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<AccountCurrency>> value,
                                                                                 Expression<Func<TEntity, List<AccountCurrency>>>          navigationExpression,
                                                                                 params string[]                                           excludeProperties) where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(AccountCurrency.Employee)))
            query = query.Include(navigationExpression)
                         .ThenInclude(accountCurrency => accountCurrency!.Employee);

        if (!excludeProperties.Contains(nameof(AccountCurrency.Account)))
            query = query.Include(navigationExpression)
                         .ThenInclude(accountCurrency => accountCurrency!.Account);

        if (!excludeProperties.Contains(nameof(AccountCurrency.Currency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(accountCurrency => accountCurrency!.Currency);

        return query;
    }
}
