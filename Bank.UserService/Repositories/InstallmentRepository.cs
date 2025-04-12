using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface IInstallmentRepository
{
    Task<Page<Installment>> FindAllByLoanId(Guid loanId, Pageable pageable);

    Task<Installment?> FindById(Guid id);

    Task<Installment> Add(Installment installment);

    Task<Installment> Update(Installment oldInstallment, Installment installment);

    Task<List<Installment>> GetDueInstallmentsForLoanAsync(Guid loanId, DateTime dueDate);

    Task<int> GetInstallmentCountForLoanAsync(Guid loanId);

    Task<int> GetPaidInstallmentsCountBeforeDateAsync(Guid loanId, DateTime date);

    Task<bool> AreAllInstallmentsPaidAsync(Guid loanId);

    Task<Installment?> GetLatestInstallmentForLoanAsync(Guid loanId);
}

public class InstallmentRepository(ApplicationContext context) : IInstallmentRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Installment>> FindAllByLoanId(Guid loanId, Pageable pageable)
    {
        var query = m_Context.Installments.IncludeAll()
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
        return await m_Context.Installments.IncludeAll()
                              .FirstOrDefaultAsync(installment => installment.Id == id);
    }

    public async Task<Installment> Add(Installment installment)
    {
        m_Context.Installments.Add(installment);
        await m_Context.SaveChangesAsync();
        return installment;
    }

    public async Task<Installment> Update(Installment oldInstallment, Installment installment)
    {
        m_Context.Entry(oldInstallment)
                 .State = EntityState.Detached;

        var updatedInstallment = m_Context.Installments.Update(installment);

        await m_Context.SaveChangesAsync();

        return updatedInstallment.Entity;
    }

    public async Task<List<Installment>> GetDueInstallmentsForLoanAsync(Guid loanId, DateTime dueDate)
    {
        return await m_Context.Installments.IncludeAll()
                              .Where(installment => installment.LoanId == loanId && installment.ExpectedDueDate.Date <= dueDate.Date &&
                                                    installment.Status == InstallmentStatus.Pending)
                              .OrderBy(installment => installment.ExpectedDueDate)
                              .ToListAsync();
    }

    public async Task<int> GetInstallmentCountForLoanAsync(Guid loanId)
    {
        return await m_Context.Installments.CountAsync(installment => installment.LoanId == loanId);
    }

    public async Task<int> GetPaidInstallmentsCountBeforeDateAsync(Guid loanId, DateTime date)
    {
        return await m_Context.Installments.CountAsync(installment => installment.LoanId          == loanId && installment.Status == InstallmentStatus.Paid &&
                                                                      installment.ExpectedDueDate < date);
    }

    public async Task<bool> AreAllInstallmentsPaidAsync(Guid loanId)
    {
        return !await m_Context.Installments.AnyAsync(installment => installment.LoanId == loanId && installment.Status == InstallmentStatus.Pending);
    }

    public async Task<Installment?> GetLatestInstallmentForLoanAsync(Guid loanId)
    {
        return await m_Context.Installments.IncludeAll()
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
                                                                                 Expression<Func<TEntity, Installment?>>          navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Installment.Loan)))
            query = query.Include(navigationExpression)
                         .ThenInclude(installment => installment!.Loan);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<Installment>> value,
                                                                                 Expression<Func<TEntity, List<Installment>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Installment.Loan)))
            query = query.Include(navigationExpression)
                         .ThenInclude(installment => installment.Loan);

        return query;
    }
}
