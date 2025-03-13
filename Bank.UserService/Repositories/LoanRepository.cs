using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface ILoanRepository
{
    Task<Page<Loan>> FindAll(LoanFilterQuery loanFilterQuery, Pageable pageable);

    Task<Loan?> FindById(Guid id);

    Task<Loan> Add(Loan loan);

    Task<Loan> Update(Loan oldLoan, Loan loan);
}

public class LoanRepository(ApplicationContext context) : ILoanRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Loan>> FindAll(LoanFilterQuery filter, Pageable pageable)
    {
        var query = this.m_Context.Loans.Include(l => l.LoanType)
                        .Include(l => l.Account)
                        .Include(l => l.Currency)
                        .AsQueryable();

        if (filter.LoanTypeId.HasValue)
        {
            query = query.Where(l => l.TypeId == filter.LoanTypeId.Value);
        }

        if (!string.IsNullOrEmpty(filter.AccountNumber))
        {
            query = query.Where(l => l.Account.Number == filter.AccountNumber);
        }

        if (!string.IsNullOrEmpty(filter.LoanStatus) && Enum.TryParse<LoanStatus>(filter.LoanStatus, out var status))
        {
            query = query.Where(l => l.Status == status);
        }

        if (filter.FromDate.HasValue)
        {
            query = query.Where(l => l.CreationDate >= filter.FromDate.Value);
        }

        if (filter.ToDate.HasValue)
        {
            query = query.Where(l => l.CreationDate <= filter.ToDate.Value);
        }

        var total = await query.CountAsync();

        var items = await query.Skip(pageable.Page * pageable.Size)
                               .Take(pageable.Size)
                               .ToListAsync();

        return new Page<Loan>(items, pageable.Page, pageable.Size, total);
    }

    public async Task<Loan?> FindById(Guid id)
    {
        return await this.m_Context.Loans.Include(l => l.LoanType)
                         .Include(l => l.Account)
                         .Include(l => l.Currency)
                         .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Loan> Add(Loan loan)
    {
        this.m_Context.Loans.Add(loan);
        await this.m_Context.SaveChangesAsync();
        return loan;
    }

    public async Task<Loan> Update(Loan oldLoan, Loan loan)
    {
        this.m_Context.Entry(oldLoan)
            .CurrentValues.SetValues(loan);

        await m_Context.SaveChangesAsync();
        return loan;
    }
}
