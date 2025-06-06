using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Database.Core;
using Bank.Permissions.Services;
using Bank.UserService.Database;
using Bank.UserService.Models;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ITransactionTemplateRepository
{
    Task<Page<TransactionTemplate>> FindAll(Pageable pageable);

    Task<TransactionTemplate?> FindById(Guid id);

    Task<TransactionTemplate> Add(TransactionTemplate transactionTemplate);

    Task<bool> AddRange(IEnumerable<TransactionTemplate> transactionTemplates);

    Task<TransactionTemplate> Update(TransactionTemplate transactionTemplate);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class TransactionTemplateRepository(IDatabaseContextFactory<ApplicationContext> contextFactory, IAuthorizationServiceFactory authorizationServiceFactory)
: ITransactionTemplateRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory              = contextFactory;
    private readonly IAuthorizationServiceFactory                m_AuthorizationServiceFactory = authorizationServiceFactory;

    public async Task<Page<TransactionTemplate>> FindAll(Pageable pageable)
    {
        await using var context              = await m_ContextFactory.CreateContext;
        var             authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        var transactionTemplateQuery = context.TransactionTemplates.IncludeAll()
                                              .AsQueryable();

        transactionTemplateQuery = transactionTemplateQuery.Where(template => template.ClientId == authorizationService.UserId);

        var transactionTemplates = await transactionTemplateQuery.Skip((pageable.Page - 1) * pageable.Size)
                                                                 .Take(pageable.Size)
                                                                 .ToListAsync();

        var totalElements = await transactionTemplateQuery.CountAsync();

        return new Page<TransactionTemplate>(transactionTemplates, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<TransactionTemplate?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.TransactionTemplates.IncludeAll()
                            .FirstOrDefaultAsync(transactionTemplate => transactionTemplate.Id == id);
    }

    public async Task<TransactionTemplate> Add(TransactionTemplate transactionTemplate)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addedTransactionTemplate = await context.TransactionTemplates.AddAsync(transactionTemplate);

        await context.SaveChangesAsync();

        return addedTransactionTemplate.Entity;
    }

    public async Task<bool> AddRange(IEnumerable<TransactionTemplate> transactionTemplates)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(transactionTemplates, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<TransactionTemplate> Update(TransactionTemplate oldTransactionTemplate, TransactionTemplate transactionTemplate)
    {
        await using var context = await m_ContextFactory.CreateContext;

        context.TransactionTemplates.Entry(oldTransactionTemplate)
               .State = EntityState.Detached;

        var updatedTransactionTemplate = context.TransactionTemplates.Update(transactionTemplate);

        await context.SaveChangesAsync();

        return updatedTransactionTemplate.Entity;
    }

    public async Task<TransactionTemplate> Update(TransactionTemplate transactionTemplate)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.TransactionTemplates.Where(dbTransactionTemplate => dbTransactionTemplate.Id == transactionTemplate.Id)
                     .ExecuteUpdateAsync(setProperty => setProperty.SetProperty(dbTransactionTemplate => dbTransactionTemplate.AccountNumber, transactionTemplate.AccountNumber)
                                                                   .SetProperty(dbTransactionTemplate => dbTransactionTemplate.Name,       transactionTemplate.Name)
                                                                   .SetProperty(dbTransactionTemplate => dbTransactionTemplate.Deleted,    transactionTemplate.Deleted)
                                                                   .SetProperty(dbTransactionTemplate => dbTransactionTemplate.ModifiedAt, transactionTemplate.ModifiedAt));

        return transactionTemplate;
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.TransactionTemplates.AnyAsync(transactionTemplate => transactionTemplate.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.TransactionTemplates.AnyAsync() is not true;
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
                                                                                 Expression<Func<TEntity, TransactionTemplate?>>          navigationExpression,
                                                                                 params string[]                                          excludeProperties) where TEntity : class
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
