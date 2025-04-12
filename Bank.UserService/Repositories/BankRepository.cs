using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using BankModel = Bank.UserService.Models.Bank;

namespace Bank.UserService.Repositories;

public interface IBankRepository
{
    Task<Page<BankModel>> FindAll(BankFilterQuery filter, Pageable pageable);

    Task<BankModel?> FindById(Guid id);
}

public class BankRepository(ApplicationContext context) : IBankRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<BankModel>> FindAll(BankFilterQuery filter, Pageable pageable)
    {
        var bankQuery = m_Context.Banks.AsQueryable();

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
        return await m_Context.Banks.FirstOrDefaultAsync(bank => bank.Id == id);
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
                                                                                 Expression<Func<TEntity, BankModel?>>          navigationExpression, params string[] excludeProperties)
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
