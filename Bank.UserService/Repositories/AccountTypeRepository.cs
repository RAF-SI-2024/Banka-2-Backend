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

public interface IAccountTypeRepository
{
    Task<Page<AccountType>> FindAll(AccountTypeFilterQuery accountTypeFilterQuery, Pageable pageable);

    Task<AccountType?> FindById(Guid id);

    Task<AccountType> Add(AccountType accountType);

    Task<bool> AddRange(IEnumerable<AccountType> accountTypes);

    Task<AccountType> Update(AccountType accountType);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class AccountTypeRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : IAccountTypeRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<Page<AccountType>> FindAll(AccountTypeFilterQuery accountTypeFilterQuery, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var accountTypeQuery = context.AccountTypes.AsQueryable();

        if (!string.IsNullOrEmpty(accountTypeFilterQuery.Name))
            accountTypeQuery = accountTypeQuery.Where(accountType => EF.Functions.ILike(accountType.Name, $"%{accountTypeFilterQuery.Name}%"));

        if (!string.IsNullOrEmpty(accountTypeFilterQuery.Name))
            accountTypeQuery = accountTypeQuery.Where(accountType => EF.Functions.ILike(accountType.Code, $"%{accountTypeFilterQuery.Code}%"));

        var accountTypes = await accountTypeQuery.Skip((pageable.Page - 1) * pageable.Size)
                                                 .Take(pageable.Size)
                                                 .ToListAsync();

        var totalElements = await accountTypeQuery.CountAsync();

        return new Page<AccountType>(accountTypes, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<AccountType?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.AccountTypes.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<AccountType> Add(AccountType accountType)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addedAccountType = await context.AccountTypes.AddAsync(accountType);

        await context.SaveChangesAsync();

        return addedAccountType.Entity;
    }

    public async Task<bool> AddRange(IEnumerable<AccountType> accountTypes)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(accountTypes, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<AccountType> Update(AccountType accountType)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.AccountTypes.Where(dbAccountType => dbAccountType.Id == accountType.Id)
                     .ExecuteUpdateAsync(setters => setters.SetProperty(dbAccountType => dbAccountType.Name, accountType.Name)
                                                           .SetProperty(dbAccountType => dbAccountType.Code,       accountType.Code)
                                                           .SetProperty(dbAccountType => dbAccountType.ModifiedAt, accountType.ModifiedAt));

        return accountType;
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.AccountTypes.AnyAsync(accountType => accountType.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.AccountTypes.AnyAsync() is not true;
    }
}

public static partial class RepositoryExtensions
{
    [Obsolete("This method does not have implementation.", true)]
    public static IIncludableQueryable<AccountType, object?> IncludeAll(this DbSet<AccountType> set)
    {
        return set.Include(accountType => accountType);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, AccountType?> value,
                                                                                 Expression<Func<TEntity, AccountType?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        return value;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<AccountType>> value,
                                                                                 Expression<Func<TEntity, List<AccountType>>>          navigationExpression,
                                                                                 params string[]                                       excludeProperties) where TEntity : class
    {
        return value;
    }
}
