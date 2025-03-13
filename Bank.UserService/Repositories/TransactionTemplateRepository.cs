using Bank.Application.Domain;
using Bank.UserService.Database;
using Bank.UserService.Models;
using Bank.UserService.Services;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface ITransactionTemplateRepository
{
    Task<Page<TransactionTemplate>> FindAll(Pageable pageable);

    Task<TransactionTemplate?> FindById(Guid id);

    Task<TransactionTemplate> Add(TransactionTemplate transactionTemplate);

    Task<TransactionTemplate> Update(TransactionTemplate oldTransactionTemplate, TransactionTemplate transactionTemplate);
}

public class TransactionTemplateRepository(ApplicationContext context, IAuthorizationService authorizationService) : ITransactionTemplateRepository
{
    private readonly ApplicationContext    m_Context              = context;
    private readonly IAuthorizationService m_AuthorizationService = authorizationService;

    public async Task<Page<TransactionTemplate>> FindAll(Pageable pageable)
    {
        var transactionTemplateQuery = m_Context.TransactionTemplates.Include(template => template.Client)
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
        return await m_Context.TransactionTemplates.Include(template => template.Client)
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
}
