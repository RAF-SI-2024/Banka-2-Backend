using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.UserService.Database;
using Bank.UserService.Models;
using Bank.UserService.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ITransactionTemplateRepository
{
    Task<Page<TransactionTemplate>> FindAll(Pageable pageable);

    Task<TransactionTemplate?> FindById(Guid id);

    Task<TransactionTemplate> Add(TransactionTemplate transactionTemplate);

    Task<TransactionTemplate> Update(TransactionTemplate transactionTemplate);
}

public class TransactionTemplateRepository(ApplicationContext context, IAuthorizationService authorizationService) : ITransactionTemplateRepository
{
    private readonly ApplicationContext    m_Context              = context;
    private readonly IAuthorizationService m_AuthorizationService = authorizationService;

    public async Task<Page<TransactionTemplate>> FindAll(Pageable pageable)
    {
        var transactionTemplateQuery = m_Context.TransactionTemplates.IncludeAll()
                                                .AsQueryable();

        transactionTemplateQuery = transactionTemplateQuery.Where(template => template.ClientId == m_AuthorizationService.UserId);

        var transactionTemplates = await transactionTemplateQuery.Skip((pageable.Page - 1) * pageable.Size)
                                                                 .Take(pageable.Size)
                                                                 .ToListAsync();

        var totalElements = await transactionTemplateQuery.CountAsync();

        return new Page<TransactionTemplate>(transactionTemplates, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<TransactionTemplate?> FindById(Guid id)
    {
        return await m_Context.TransactionTemplates.IncludeAll()
                              .FirstOrDefaultAsync(transactionTemplate => transactionTemplate.Id == id);
    }

    public async Task<TransactionTemplate> Add(TransactionTemplate transactionTemplate)
    {
        var addedTransactionTemplate = await m_Context.TransactionTemplates.AddAsync(transactionTemplate);

        await m_Context.SaveChangesAsync();

        return addedTransactionTemplate.Entity;
    }

    public async Task<TransactionTemplate> Update(TransactionTemplate oldTransactionTemplate, TransactionTemplate transactionTemplate)
    {
        m_Context.TransactionTemplates.Entry(oldTransactionTemplate)
                 .State = EntityState.Detached;

        var updatedTransactionTemplate = m_Context.TransactionTemplates.Update(transactionTemplate);

        await m_Context.SaveChangesAsync();

        return updatedTransactionTemplate.Entity;
    }

    public async Task<TransactionTemplate> Update(TransactionTemplate transactionTemplate)
    {
        await m_Context.TransactionTemplates.Where(dbTransactionTemplate => dbTransactionTemplate.Id == transactionTemplate.Id)
                       .ExecuteUpdateAsync(setProperty => setProperty.SetProperty(dbTransactionTemplate => dbTransactionTemplate.AccountNumber, transactionTemplate.AccountNumber)
                                                                     .SetProperty(dbTransactionTemplate => dbTransactionTemplate.Name,       transactionTemplate.Name)
                                                                     .SetProperty(dbTransactionTemplate => dbTransactionTemplate.Deleted,    transactionTemplate.Deleted)
                                                                     .SetProperty(dbTransactionTemplate => dbTransactionTemplate.ModifiedAt, transactionTemplate.ModifiedAt));

        return transactionTemplate;
    }
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<TransactionTemplate, object?> IncludeAll(this DbSet<TransactionTemplate> set)
    {
        return set.Include(transactionTemplate => transactionTemplate.Client)
                  .ThenIncludeAll(transactionTemplate => transactionTemplate.Client, nameof(User.TransactionTemplates));
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, TransactionTemplate?> value,
                                                                                 Expression<Func<TEntity, TransactionTemplate?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(TransactionTemplate.Client)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transactionTemplate => transactionTemplate!.Client);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<TransactionTemplate>> value,
                                                                                 Expression<Func<TEntity, List<TransactionTemplate>>> navigationExpression,
                                                                                 params string[] excludeProperties) where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(TransactionTemplate.Client)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transactionTemplate => transactionTemplate.Client);

        return query;
    }
}
