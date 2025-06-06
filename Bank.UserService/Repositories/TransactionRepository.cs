using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Database.Core;
using Bank.Permissions.Services;
using Bank.UserService.Database;
using Bank.UserService.Models;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ITransactionRepository
{
    Task<Page<Transaction>> FindAll(TransactionFilterQuery filter, Pageable pageable);

    Task<Page<Transaction>> FindAllByAccountId(Guid accountId, TransactionFilterQuery filter, Pageable pageable);

    Task<Transaction?> FindById(Guid id);

    Task<Transaction> Add(Transaction transaction);

    Task<bool> AddRange(IEnumerable<Transaction> transactions);

    Task<Transaction> Update(Transaction transaction);

    Task<bool> UpdateStatus(Guid id, TransactionStatus status);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class TransactionRepository(IDatabaseContextFactory<ApplicationContext> contextFactory, IAuthorizationServiceFactory authorizationServiceFactory) : ITransactionRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory              = contextFactory;
    private readonly IAuthorizationServiceFactory                m_AuthorizationServiceFactory = authorizationServiceFactory;

    public async Task<Page<Transaction>> FindAll(TransactionFilterQuery filter, Pageable pageable)
    {
        await using var context              = await m_ContextFactory.CreateContext;
        var             authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        var transactionQuery = context.Transactions.IncludeAll()
                                      .AsQueryable();

        if (authorizationService.Permissions == Permission.Client)
            transactionQuery = transactionQuery.Where(transaction => transaction.FromAccount!.ClientId == authorizationService.UserId ||
                                                                     transaction.ToAccount!.ClientId   == authorizationService.UserId);

        if (filter.Status != TransactionStatus.Invalid)
            transactionQuery = transactionQuery.Where(transaction => transaction.Status == filter.Status);

        if (filter.FromDate != DateOnly.MinValue)
            transactionQuery = transactionQuery.Where(transaction => DateOnly.FromDateTime(transaction.CreatedAt) >= filter.FromDate);

        if (filter.ToDate != DateOnly.MinValue)
            transactionQuery = transactionQuery.Where(transaction => DateOnly.FromDateTime(transaction.CreatedAt) <= filter.ToDate);

        transactionQuery = transactionQuery.OrderByDescending(transaction => transaction.CreatedAt);

        var transactions = await transactionQuery.Skip((pageable.Page - 1) * pageable.Size)
                                                 .Take(pageable.Size)
                                                 .ToListAsync();

        var totalElements = await transactionQuery.CountAsync();

        return new Page<Transaction>(transactions, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Page<Transaction>> FindAllByAccountId(Guid accountId, TransactionFilterQuery filter, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var transactionQuery = context.Transactions.IncludeAll()
                                      .AsQueryable();

        if (filter.Status != TransactionStatus.Invalid)
            transactionQuery = transactionQuery.Where(transaction => transaction.Status == filter.Status);

        if (filter.FromDate != DateOnly.MinValue)
            transactionQuery = transactionQuery.Where(transaction => DateOnly.FromDateTime(transaction.CreatedAt) >= filter.FromDate);

        if (filter.ToDate != DateOnly.MinValue)
            transactionQuery = transactionQuery.Where(transaction => DateOnly.FromDateTime(transaction.CreatedAt) <= filter.ToDate);

        transactionQuery = transactionQuery.Where(transaction => transaction.FromAccountId == accountId || transaction.ToAccountId == accountId);

        transactionQuery = transactionQuery.OrderByDescending(transaction => transaction.CreatedAt);

        var transactions = await transactionQuery.Skip((pageable.Page - 1) * pageable.Size)
                                                 .Take(pageable.Size)
                                                 .ToListAsync();

        var totalElements = await transactionQuery.CountAsync();

        return new Page<Transaction>(transactions, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Transaction?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await FindById(id, context);
    }

    public async Task<Transaction> Add(Transaction transaction)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addedTransaction = await context.Transactions.AddAsync(transaction);

        await context.SaveChangesAsync();

        return addedTransaction.Entity;
    }

    public async Task<bool> AddRange(IEnumerable<Transaction> transactions)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(transactions, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<Transaction> Update(Transaction transaction)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.Transactions.Where(dbTransaction => dbTransaction.Id == transaction.Id)
                     .ExecuteUpdateAsync(setters => setters.SetProperty(dbTransaction => dbTransaction.Status, transaction.Status)
                                                           .SetProperty(dbTransaction => dbTransaction.ModifiedAt, transaction.ModifiedAt));

        return transaction;
    }

    public async Task<bool> UpdateStatus(Guid id, TransactionStatus status)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await UpdateStatus(id, status, context);
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Transactions.AnyAsync(transaction => transaction.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Transactions.AnyAsync() is not true;
    }

    public static async Task<bool> UpdateStatus(Guid id, TransactionStatus status, ApplicationContext context)
    {
        var updatedRows = await context.Transactions.Where(transaction => transaction.Id == id)
                                       .ExecuteUpdateAsync(setProperty => setProperty.SetProperty(transaction => transaction.Status, status)
                                                                                     .SetProperty(transaction => transaction.ModifiedAt, DateTime.UtcNow));

        return updatedRows == 1;
    }

    public static async Task<Transaction?> FindById(Guid id, ApplicationContext context)
    {
        return await context.Transactions.IncludeAll()
                            .FirstOrDefaultAsync(transaction => transaction.Id == id);
    }
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<Transaction, object?> IncludeAll(this DbSet<Transaction> set)
    {
        return set.Include(transaction => transaction.FromAccount)
                  .ThenIncludeAll(transaction => transaction.FromAccount)
                  .Include(transaction => transaction.FromAccount)
                  .ThenInclude(account => account!.Client)
                  .ThenInclude(client => client!.Bank)
                  .Include(transaction => transaction.FromCurrency)
                  .ThenIncludeAll(transaction => transaction.FromCurrency)
                  .Include(transaction => transaction.ToAccount)
                  .ThenIncludeAll(transaction => transaction.ToAccount)
                  .Include(transaction => transaction.ToAccount)
                  .ThenInclude(account => account!.Client)
                  .ThenInclude(client => client!.Bank)
                  .Include(transaction => transaction.ToCurrency)
                  .ThenIncludeAll(transaction => transaction.ToCurrency)
                  .Include(transaction => transaction.Code)
                  .ThenIncludeAll(transaction => transaction.Code);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, Transaction?> value,
                                                                                 Expression<Func<TEntity, Transaction?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Transaction.FromAccount)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transaction => transaction!.FromAccount);

        if (!excludeProperties.Contains(nameof(Transaction.FromCurrency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transaction => transaction!.FromCurrency);

        if (!excludeProperties.Contains(nameof(Transaction.ToAccount)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transaction => transaction!.ToAccount);

        if (!excludeProperties.Contains(nameof(Transaction.ToCurrency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transaction => transaction!.ToCurrency);

        if (!excludeProperties.Contains(nameof(Transaction.Code)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transaction => transaction!.Code);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<Transaction>> value,
                                                                                 Expression<Func<TEntity, List<Transaction>>>          navigationExpression,
                                                                                 params string[]                                       excludeProperties) where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Transaction.FromAccount)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transaction => transaction.FromAccount);

        if (!excludeProperties.Contains(nameof(Transaction.FromCurrency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transaction => transaction.FromCurrency);

        if (!excludeProperties.Contains(nameof(Transaction.ToAccount)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transaction => transaction.ToAccount);

        if (!excludeProperties.Contains(nameof(Transaction.ToCurrency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transaction => transaction.ToCurrency);

        if (!excludeProperties.Contains(nameof(Transaction.Code)))
            query = query.Include(navigationExpression)
                         .ThenInclude(transaction => transaction.Code);

        return query;
    }
}
