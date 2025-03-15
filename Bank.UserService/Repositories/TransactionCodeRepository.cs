using Bank.Application.Domain;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

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
