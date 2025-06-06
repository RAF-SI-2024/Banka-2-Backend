using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Database.Core;
using Bank.UserService.Database;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using BankModel = Bank.UserService.Models.Bank;

namespace Bank.UserService.Repositories;

public interface IBankRepository
{
    Task<Page<BankModel>> FindAll(BankFilterQuery filter, Pageable pageable);

    Task<BankModel?> FindById(Guid id);

    Task<bool> AddRange(IEnumerable<BankModel> banks);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class BankRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : IBankRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<Page<BankModel>> FindAll(BankFilterQuery filter, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var bankQuery = context.Banks.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            bankQuery = bankQuery.Where(bank => EF.Functions.ILike(bank.Name, $"%{filter.Name}%"));

        if (!string.IsNullOrEmpty(filter.Code))
            bankQuery = bankQuery.Where(bank => EF.Functions.ILike(bank.Code, $"%{filter.Code}%"));

        var banks = await bankQuery.Skip((pageable.Page - 1) * pageable.Size)
                                   .Take(pageable.Size)
                                   .ToListAsync();

        var totalElements = await bankQuery.CountAsync();

        return new Page<BankModel>(banks, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<BankModel?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Banks.FirstOrDefaultAsync(bank => bank.Id == id);
    }

    public async Task<bool> AddRange(IEnumerable<BankModel> banks)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(banks, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Banks.AnyAsync(bank => bank.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Banks.AnyAsync() is not true;
    }
}

public static partial class RepositoryExtensions
{
    [Obsolete("This method does not have implementation.", true)]
    public static IIncludableQueryable<BankModel, object?> IncludeAll(this DbSet<BankModel> set)
    {
        return set.Include(bank => bank);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, BankModel?> query,
                                                                                 Expression<Func<TEntity, BankModel?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<BankModel>> query,
                                                                                 Expression<Func<TEntity, List<BankModel>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        return query;
    }
}
