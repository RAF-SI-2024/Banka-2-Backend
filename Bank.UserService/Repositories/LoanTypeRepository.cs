using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Database.Core;
using Bank.UserService.Database;
using Bank.UserService.Models;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ILoanTypeRepository
{
    Task<Page<LoanType>> FindAll(Pageable pageable);

    Task<LoanType?> FindById(Guid id);

    Task<LoanType> Add(LoanType loanType);

    Task<bool> AddRange(IEnumerable<LoanType> loanTypes);

    Task<LoanType> Update(LoanType loanType);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class LoanTypeRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : ILoanTypeRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<Page<LoanType>> FindAll(Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var query = context.LoanTypes.AsQueryable();

        var total = await query.CountAsync();

        var items = await query.Skip((pageable.Page - 1) * pageable.Size)
                               .Take(pageable.Size)
                               .ToListAsync();

        return new Page<LoanType>(items, pageable.Page, pageable.Size, total);
    }

    public async Task<LoanType?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.LoanTypes.FirstOrDefaultAsync(loanType => loanType.Id == id);
    }

    public async Task<LoanType> Add(LoanType loanType)
    {
        await using var context = await m_ContextFactory.CreateContext;

        context.LoanTypes.Add(loanType);
        await context.SaveChangesAsync();
        return loanType;
    }

    public async Task<bool> AddRange(IEnumerable<LoanType> loanTypes)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(loanTypes, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<LoanType> Update(LoanType loanType)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.LoanTypes.Where(dbLoanType => dbLoanType.Id == loanType.Id)
                     .ExecuteUpdateAsync(setters => setters.SetProperty(dbLoanType => dbLoanType.Name, loanType.Name)
                                                           .SetProperty(dbLoanType => dbLoanType.Margin,     loanType.Margin)
                                                           .SetProperty(dbLoanType => dbLoanType.ModifiedAt, loanType.ModifiedAt));

        return loanType;
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.LoanTypes.AnyAsync(loanType => loanType.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.LoanTypes.AnyAsync() is not true;
    }
}

public static partial class RepositoryExtensions
{
    [Obsolete("This method does not have implementation.", true)]
    public static IIncludableQueryable<LoanType, object?> IncludeAll(this DbSet<LoanType> set)
    {
        return set.Include(loanType => loanType);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, LoanType?> value,
                                                                                 Expression<Func<TEntity, LoanType?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        return value;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<LoanType>> value,
                                                                                 Expression<Func<TEntity, List<LoanType>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        return value;
    }
}
