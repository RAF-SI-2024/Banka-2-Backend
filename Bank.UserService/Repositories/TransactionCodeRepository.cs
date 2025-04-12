using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ITransactionCodeRepository
{
    Task<Page<TransactionCode>> FindAll(Pageable pageable);

    Task<TransactionCode?> FindById(Guid id);
}

public class TransactionCodeRepository(ApplicationContext context) : ITransactionCodeRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<TransactionCode>> FindAll(Pageable pageable)
    {
        var transactionCodeQuery = m_Context.TransactionCodes.AsQueryable();

        var transactionCodes = await transactionCodeQuery.Skip((pageable.Page - 1) * pageable.Size)
                                                         .Take(pageable.Size)
                                                         .ToListAsync();

        var totalElements = await transactionCodeQuery.CountAsync();

        return new Page<TransactionCode>(transactionCodes, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<TransactionCode?> FindById(Guid id)
    {
        return await m_Context.TransactionCodes.FirstOrDefaultAsync(transactionCode => transactionCode.Id == id);
    }
}

public static partial class RepositoryExtensions
{
    [Obsolete("This method does not have implementation.", true)]
    public static IIncludableQueryable<TransactionCode, object?> IncludeAll(this DbSet<TransactionCode> set)
    {
        return set.Include(transactionCode => transactionCode);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, TransactionCode?> value,
                                                                                 Expression<Func<TEntity, TransactionCode?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        return value;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<TransactionCode>> value,
                                                                                 Expression<Func<TEntity, List<TransactionCode>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        return value;
    }
}
