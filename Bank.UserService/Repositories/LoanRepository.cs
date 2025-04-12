using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ILoanRepository
{
    Task<Page<Loan>> FindAll(LoanFilterQuery loanFilterQuery, Pageable pageable);

    Task<Loan?> FindById(Guid id);

    Task<Loan> Add(Loan loan);

    Task<Loan> Update(Loan loan);

    Task<List<Loan>> GetLoansWithDueInstallmentsAsync(DateTime dueDate);

    Task<Page<Loan>> FindByClientId(Guid clientId, Pageable pageable);
}

public class LoanRepository(ApplicationContext context) : ILoanRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Loan>> FindAll(LoanFilterQuery filter, Pageable pageable)
    {
        var query = m_Context.Loans.IncludeAll()
                             .AsQueryable();

        if (filter.LoanTypeId.HasValue)
            query = query.Where(l => l.TypeId == filter.LoanTypeId.Value);

        if (!string.IsNullOrEmpty(filter.AccountNumber))
            query = query.Where(l => l.Account.Number == filter.AccountNumber);

        if (!string.IsNullOrEmpty(filter.LoanStatus) && Enum.TryParse<LoanStatus>(filter.LoanStatus, out var status))
            query = query.Where(l => l.Status == status);

        if (filter.FromDate.HasValue)
            query = query.Where(l => l.CreationDate >= filter.FromDate.Value);

        if (filter.ToDate.HasValue)
            query = query.Where(l => l.CreationDate <= filter.ToDate.Value);

        var total = await query.CountAsync();

        var items = await query.Skip((pageable.Page - 1) * pageable.Size)
                               .Take(pageable.Size)
                               .ToListAsync();

        return new Page<Loan>(items, pageable.Page, pageable.Size, total);
    }

    public async Task<Loan?> FindById(Guid id)
    {
        return await m_Context.Loans.IncludeAll()
                              .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Loan> Add(Loan loan)
    {
        m_Context.Loans.Add(loan);
        await m_Context.SaveChangesAsync();
        return loan;
    }

    public async Task<Loan> Update(Loan loan)
    {
        await m_Context.Loans.Where(dbLoan => dbLoan.Id == loan.Id)
                       .ExecuteUpdateAsync(setters => setters.SetProperty(dbLoan => dbLoan.TypeId, loan.TypeId)
                                                             .SetProperty(dbLoan => dbLoan.AccountId,    loan.AccountId)
                                                             .SetProperty(dbLoan => dbLoan.Amount,       loan.Amount)
                                                             .SetProperty(dbLoan => dbLoan.Period,       loan.Period)
                                                             .SetProperty(dbLoan => dbLoan.CreationDate, loan.CreationDate)
                                                             .SetProperty(dbLoan => dbLoan.MaturityDate, loan.MaturityDate)
                                                             .SetProperty(dbLoan => dbLoan.CurrencyId,   loan.CurrencyId)
                                                             .SetProperty(dbLoan => dbLoan.Status,       loan.Status)
                                                             .SetProperty(dbLoan => dbLoan.InterestType, loan.InterestType)
                                                             .SetProperty(dbLoan => dbLoan.ModifiedAt,   loan.ModifiedAt));

        return loan;
    }

    public async Task<List<Loan>> GetLoansWithDueInstallmentsAsync(DateTime dueDate)
    {
        return await m_Context.Loans.IncludeAll()
                              .Where(loan => loan.Status == LoanStatus.Active &&
                                             loan.Installments.Any(installment => installment.ExpectedDueDate.Date <= dueDate.Date &&
                                                                                  installment.Status               == InstallmentStatus.Pending))
                              .ToListAsync();
    }

    public async Task<Page<Loan>> FindByClientId(Guid clientId, Pageable pageable)
    {
        var query = m_Context.Loans.IncludeAll()
                             .Where(loan => loan.Account.ClientId == clientId)
                             .AsQueryable();

        var total = await query.CountAsync();

        var items = await query.Skip((pageable.Page - 1) * pageable.Size)
                               .Take(pageable.Size)
                               .ToListAsync();

        return new Page<Loan>(items, pageable.Page, pageable.Size, total);
    }
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<Loan, object?> IncludeAll(this DbSet<Loan> set)
    {
        return set.Include(loan => loan.LoanType)
                  .ThenIncludeAll(loan => loan.LoanType)
                  .Include(loan => loan.Account)
                  .ThenIncludeAll(loan => loan.Account)
                  .Include(loan => loan.Currency)
                  .ThenIncludeAll(loan => loan.Currency)
                  .Include(loan => loan.Installments)
                  .ThenIncludeAll(loan => loan.Installments, nameof(Installment.Loan));
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, Loan?> value,
                                                                                 Expression<Func<TEntity, Loan?>>          navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Loan.LoanType)))
            query = query.Include(navigationExpression)
                         .ThenInclude(loan => loan!.LoanType);

        if (!excludeProperties.Contains(nameof(Loan.Account)))
            query = query.Include(navigationExpression)
                         .ThenInclude(loan => loan!.Account);

        if (!excludeProperties.Contains(nameof(Loan.Currency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(loan => loan!.Currency);

        if (!excludeProperties.Contains(nameof(Loan.Installments)))
            query = query.Include(navigationExpression)
                         .ThenInclude(loan => loan!.Installments);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<Loan>> value,
                                                                                 Expression<Func<TEntity, List<Loan>>>          navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Loan.LoanType)))
            query = query.Include(navigationExpression)
                         .ThenInclude(loan => loan.LoanType);

        if (!excludeProperties.Contains(nameof(Loan.Account)))
            query = query.Include(navigationExpression)
                         .ThenInclude(loan => loan.Account);

        if (!excludeProperties.Contains(nameof(Loan.Currency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(loan => loan.Currency);

        if (!excludeProperties.Contains(nameof(Loan.Installments)))
            query = query.Include(navigationExpression)
                         .ThenInclude(loan => loan.Installments);

        return query;
    }
}
