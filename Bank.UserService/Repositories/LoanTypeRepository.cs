using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ILoanTypeRepository
{
    Task<Page<LoanType>> FindAll(Pageable pageable);

    Task<LoanType?> FindById(Guid id);

    Task<LoanType> Add(LoanType loanType);

    Task<LoanType> Update(LoanType loanType);
}

public class LoanTypeRepository(ApplicationContext context) : ILoanTypeRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<LoanType>> FindAll(Pageable pageable)
    {
        var query = m_Context.LoanTypes.AsQueryable();

        var total = await query.CountAsync();

        var items = await query.Skip((pageable.Page - 1) * pageable.Size)
                               .Take(pageable.Size)
                               .ToListAsync();

        return new Page<LoanType>(items, pageable.Page, pageable.Size, total);
    }

    public async Task<LoanType?> FindById(Guid id)
    {
        return await m_Context.LoanTypes.FirstOrDefaultAsync(loanType => loanType.Id == id);
    }

    public async Task<LoanType> Add(LoanType loanType)
    {
        m_Context.LoanTypes.Add(loanType);
        await m_Context.SaveChangesAsync();
        return loanType;
    }

    public async Task<LoanType> Update(LoanType loanType)
    {
        await m_Context.LoanTypes.Where(dbLoanType => dbLoanType.Id == loanType.Id)
                       .ExecuteUpdateAsync(setters => setters.SetProperty(dbLoanType => dbLoanType.Name, loanType.Name)
                                                             .SetProperty(dbLoanType => dbLoanType.Margin,     loanType.Margin)
                                                             .SetProperty(dbLoanType => dbLoanType.ModifiedAt, loanType.ModifiedAt));

        return loanType;
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
                                                                                 Expression<Func<TEntity, LoanType?>>          navigationExpression, params string[] excludeProperties)
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
