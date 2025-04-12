using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface IAccountCurrencyRepository
{
    Task<Page<AccountCurrency>> FindAll(Pageable pageable);

    Task<AccountCurrency?> FindById(Guid id);

    Task<bool> Exists(Guid id);

    Task<AccountCurrency> Add(AccountCurrency accountCurrency);

    Task<AccountCurrency> Update(AccountCurrency accountCurrency);
}

public class AccountCurrencyRepository(ApplicationContext context, IDbContextFactory<ApplicationContext> contextFactory) : IAccountCurrencyRepository
{
    private readonly ApplicationContext                    m_Context        = context;
    private readonly IDbContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    private Task<ApplicationContext> CreateContext => m_ContextFactory.CreateDbContextAsync();

    public async Task<Page<AccountCurrency>> FindAll(Pageable pageable)
    {
        var x = m_Context.AccountCurrencies.Include(accountCurrency => accountCurrency.Employee);

        var accountTypeQuery = m_Context.AccountCurrencies.IncludeAll()
                                        .AsQueryable();

        var accountTypes = await accountTypeQuery.Skip((pageable.Page - 1) * pageable.Size)
                                                 .Take(pageable.Size)
                                                 .ToListAsync();

        var totalElements = await accountTypeQuery.CountAsync();

        return new Page<AccountCurrency>(accountTypes, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<AccountCurrency?> FindById(Guid id)
    {
        return await m_Context.AccountCurrencies.IncludeAll()
                              .FirstOrDefaultAsync(accountCurrency => accountCurrency.Id == id);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await m_Context.AccountCurrencies.AnyAsync(accountCurrency => accountCurrency.Id == id);
    }

    public async Task<AccountCurrency> Add(AccountCurrency accountCurrency)
    {
        var addedAccountCurrency = await m_Context.AccountCurrencies.AddAsync(accountCurrency);

        await m_Context.SaveChangesAsync();

        return addedAccountCurrency.Entity;
    }

    public async Task<AccountCurrency> Update(AccountCurrency accountCurrency)
    {
        await m_Context.AccountCurrencies.Where(dbAccountCurrency => dbAccountCurrency.Id == accountCurrency.Id)
                       .ExecuteUpdateAsync(setters => setters.SetProperty(dbAccountCurrency => dbAccountCurrency.DailyLimit, accountCurrency.DailyLimit)
                                                             .SetProperty(dbAccountCurrency => dbAccountCurrency.MonthlyLimit, accountCurrency.MonthlyLimit)
                                                             .SetProperty(dbAccountCurrency => dbAccountCurrency.ModifiedAt,   accountCurrency.ModifiedAt));

        return accountCurrency;
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
                                                                                 Expression<Func<TEntity, AccountCurrency?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
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
                                                                                 Expression<Func<TEntity, List<AccountCurrency>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
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
