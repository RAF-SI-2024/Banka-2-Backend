using Bank.Application.Domain;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface IInstallmentRepository
{
    Task<Page<Installment>> FindAllByLoanId(Guid loanId, Pageable pageable);

    Task<Installment?> FindById(Guid id);

    Task<Installment> Add(Installment installment);

    Task<Installment> Update(Installment oldInstallment, Installment installment);
}

public class InstallmentRepository(ApplicationContext context) : IInstallmentRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Installment>> FindAllByLoanId(Guid loanId, Pageable pageable)
    {
        var query = m_Context.Installments.Where(i => i.LoanId == loanId)
                             .OrderBy(i => i.ExpectedDueDate);

        var total = await query.CountAsync();

        var items = await query.Skip(pageable.Page * pageable.Size)
                               .Take(pageable.Size)
                               .ToListAsync();

        return new Page<Installment>(items, pageable.Page, pageable.Size, total);
    }

    public async Task<Installment?> FindById(Guid id)
    {
        return await m_Context.Installments.Include(i => i.Loan)
                              .FirstOrDefaultAsync(i => i.Id == id);
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
                 .CurrentValues.SetValues(installment);

        await m_Context.SaveChangesAsync();
        return installment;
    }
}
