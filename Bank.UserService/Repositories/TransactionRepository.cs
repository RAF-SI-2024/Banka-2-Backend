using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;
using Bank.UserService.Services;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface ITransactionRepository
{
    Task<Page<Transaction>> FindAll(TransactionFilterQuery filter, Pageable pageable);

    Task<Transaction?> FindById(Guid id);

    Task<Transaction> Add(Transaction transaction);

    Task<Transaction> Update(Transaction oldTransaction, Transaction transaction);

    Task<Transaction> UpdateStatus(Guid id, TransactionStatus status);
}

public class TransactionRepository(ApplicationContext context, IAuthorizationService authorizationService) : ITransactionRepository
{
    private readonly ApplicationContext    m_Context              = context;
    private readonly IAuthorizationService m_AuthorizationService = authorizationService;

    public async Task<Page<Transaction>> FindAll(TransactionFilterQuery filter, Pageable pageable)
    {
        var transactionQuery = m_Context.Transactions.Include(transaction => transaction.FromAccount)
                                        .Include(transaction => transaction.ToAccount)
                                        .Include(transaction => transaction.Code)
                                        .AsQueryable();

        if (m_AuthorizationService.Role == Role.Client)
            transactionQuery = transactionQuery.Where(transaction => transaction.FromAccount!.ClientId == m_AuthorizationService.UserId ||
                                                                     transaction.ToAccount!.ClientId   == m_AuthorizationService.UserId);

        if (filter.Status != TransactionStatus.Invalid)
            transactionQuery = transactionQuery.Where(transaction => transaction.Status == filter.Status);

        if (filter.FromDate != DateOnly.MinValue)
            transactionQuery = transactionQuery.Where(transaction => DateOnly.FromDateTime(transaction.CreatedAt) >= filter.FromDate);

        if (filter.ToDate != DateOnly.MinValue)
            transactionQuery = transactionQuery.Where(transaction => DateOnly.FromDateTime(transaction.CreatedAt) <= filter.ToDate);

        var transactions = await transactionQuery.Skip((pageable.Page - 1) * pageable.Size)
                                                 .Take(pageable.Size)
                                                 .ToListAsync();

        var totalElements = await transactionQuery.CountAsync();

        return new Page<Transaction>(transactions, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Transaction?> FindById(Guid id)
    {
        return await m_Context.Transactions.Include(transaction => transaction.FromAccount)
                              .Include(transaction => transaction.ToAccount)
                              .Include(transaction => transaction.Code)
                              .FirstOrDefaultAsync(transaction => transaction.Id == id);
    }

    public async Task<Transaction> Add(Transaction transaction)
    {
        var addedTransaction = await m_Context.Transactions.AddAsync(transaction);

        await m_Context.SaveChangesAsync();

        return addedTransaction.Entity;
    }

    public async Task<Transaction> Update(Transaction oldTransaction, Transaction transaction)
    {
        m_Context.Transactions.Entry(oldTransaction)
                 .State = EntityState.Detached;

        var updatedTransaction = m_Context.Transactions.Update(transaction);

        await m_Context.SaveChangesAsync();

        return updatedTransaction.Entity;
    }

    public async Task<Transaction> UpdateStatus(Guid id, TransactionStatus status)
    {
        await m_Context.Transactions.Where(transaction => transaction.Id == id)
                       .ExecuteUpdateAsync(setProperty => setProperty.SetProperty(transaction => transaction.Status, status));

        var transaction = await m_Context.Transactions.AsNoTracking()
                                         .FirstOrDefaultAsync(transaction => transaction.Id == id);

        return transaction!;
    }
}
