using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Database.Core;
using Bank.UserService.Database;
using Bank.UserService.Models;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface IInstallmentRepository
{
    Task<Page<Installment>> FindAllByLoanId(Guid loanId, Pageable pageable);

    Task<Installment?> FindById(Guid id);

    Task<Installment> Add(Installment installment);

    Task<bool> AddRange(IEnumerable<Installment> installments);

    Task<Installment> Update(Installment oldInstallment, Installment installment);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();

    Task<List<Installment>> GetDueInstallmentsForLoanAsync(Guid loanId, DateTime dueDate);

    Task<int> GetInstallmentCountForLoanAsync(Guid loanId);

    Task<int> GetPaidInstallmentsCountBeforeDateAsync(Guid loanId, DateTime date);

    Task<bool> AreAllInstallmentsPaidAsync(Guid loanId);

    Task<Installment?> GetLatestInstallmentForLoanAsync(Guid loanId);
}

public class InstallmentRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : IInstallmentRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<Page<Installment>> FindAllByLoanId(Guid loanId, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var query = context.Installments.IncludeAll()
                           .Where(installment => installment.LoanId == loanId)
                           .OrderBy(i => i.ExpectedDueDate);

        var total = await query.CountAsync();

        var items = await query.Skip((pageable.Page - 1) * pageable.Size)
                               .Take(pageable.Size)
                               .ToListAsync();

        return new Page<Installment>(items, pageable.Page, pageable.Size, total);
    }

    public async Task<Installment?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Installments.IncludeAll()
                            .FirstOrDefaultAsync(installment => installment.Id == id);
    }

    public async Task<Installment> Add(Installment installment)
    {
        await using var context = await m_ContextFactory.CreateContext;

        context.Installments.Add(installment);
        await context.SaveChangesAsync();
        return await context.Installments.IncludeAll()
                                    .FirstAsync(i => i.Id == installment.Id);;
    }

    public async Task<bool> AddRange(IEnumerable<Installment> installments)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(installments, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<Installment> Update(Installment oldInstallment, Installment installment)
    {
        await using var context = await m_ContextFactory.CreateContext;

        context.Entry(oldInstallment)
               .State = EntityState.Detached;

        var updatedInstallment = context.Installments.Update(installment);

        await context.SaveChangesAsync();

        return updatedInstallment.Entity;
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Installments.AnyAsync(installment => installment.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Installments.AnyAsync() is not true;
    }

    public async Task<List<Installment>> GetDueInstallmentsForLoanAsync(Guid loanId, DateTime dueDate)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Installments.IncludeAll()
                            .Where(installment => installment.LoanId == loanId && installment.ExpectedDueDate.Date <= dueDate.Date &&
                                                  installment.Status == InstallmentStatus.Pending)
                            .OrderBy(installment => installment.ExpectedDueDate)
                            .ToListAsync();
    }

    public async Task<int> GetInstallmentCountForLoanAsync(Guid loanId)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Installments.CountAsync(installment => installment.LoanId == loanId);
    }

    public async Task<int> GetPaidInstallmentsCountBeforeDateAsync(Guid loanId, DateTime date)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Installments.CountAsync(installment => installment.LoanId          == loanId && installment.Status == InstallmentStatus.Paid &&
                                                                    installment.ExpectedDueDate < date);
    }

    public async Task<bool> AreAllInstallmentsPaidAsync(Guid loanId)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return !await context.Installments.AnyAsync(installment => installment.LoanId == loanId && installment.Status == InstallmentStatus.Pending);
    }

    public async Task<Installment?> GetLatestInstallmentForLoanAsync(Guid loanId)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Installments.IncludeAll()
                            .Where(installment => installment.LoanId == loanId)
                            .OrderByDescending(installment => installment.ExpectedDueDate)
                            .FirstOrDefaultAsync();
    }
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<Installment, object?> IncludeAll(this DbSet<Installment> set)
    {
        return set.Include(installment => installment.Loan)
                  .ThenIncludeAll(installment => installment.Loan, nameof(Loan.Installments));
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, Installment?> value,
                                                                                 Expression<Func<TEntity, Installment?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Installment.Loan)))
            query = query.Include(navigationExpression)
                         .ThenInclude(installment => installment!.Loan);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<Installment>> value,
                                                                                 Expression<Func<TEntity, List<Installment>>>          navigationExpression,
                                                                                 params string[]                                       excludeProperties) where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Installment.Loan)))
            query = query.Include(navigationExpression)
                         .ThenInclude(installment => installment.Loan);

        return query;
    }
}
