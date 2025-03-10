using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface ICreditRepository
{
    Task<Page<Credit>> FindAll(CreditFilterQuery  creditFilterQuery, Pageable pageable);
    Task<Credit?>      FindById(Guid              id);
    Task<List<Credit>> FindByAccountNumber(string accountNumber);
    Task<Credit?>      FindByCreditNumber(int     creditNumber);
    Task<Credit>      Add(Credit                 credit);
    Task<Credit>       Update(Credit              oldCredit, Credit credit);
    Task<Credit>       UpdateStatus(Guid          id,        string status);
    Task<Credit>       ApproveCreditRequest(Guid  id);
    Task<Credit>       RejectCreditRequest(Guid   id);
}

public class CreditRepository(ApplicationContext context) : ICreditRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Credit>> FindAll(CreditFilterQuery creditFilterQuery, Pageable pageable)
    {
        var creditQuery = m_Context.Credits.Include(c => c.CreditType)
                                   .Include(c => c.Account)
                                   .Include(c => c.Currency)
                                   .Include(c => c.InterestType)
                                   .AsQueryable();

        // Apply filters based on creditFilterQuery
        if (creditFilterQuery.CreditTypeId.HasValue)
            creditQuery = creditQuery.Where(credit => credit.CreditTypeId == creditFilterQuery.CreditTypeId.Value);

        if (!string.IsNullOrEmpty(creditFilterQuery.AccountNumber))
            creditQuery = creditQuery.Where(credit => credit.AccountNumber == creditFilterQuery.AccountNumber);

        if (!string.IsNullOrEmpty(creditFilterQuery.CreditStatus))
            creditQuery = creditQuery.Where(credit => credit.CreditStatus == creditFilterQuery.CreditStatus);


        creditQuery = creditQuery.Where(c => c.CreditStatus == "Pending")
                                 .OrderByDescending(c => c.ContractDate);
        
        int totalElements = await creditQuery.CountAsync();

        var credits = await creditQuery.Skip((pageable.Page - 1) * pageable.Size)
                                      .Take(pageable.Size)
                                      .ToListAsync();

        return new Page<Credit>(credits, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Credit?> FindById(Guid id)
    {
        return await m_Context.Credits.Include(c => c.CreditType)
                               .Include(c => c.Account)
                               .Include(c => c.Currency)
                               .Include(c => c.InterestType)
                               .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Credit>> FindByAccountNumber(string accountNumber)
    {
        return await m_Context.Credits.Include(credit => credit.CreditType)
                               .Include(credit => credit.Currency)
                               .Include(credit => credit.InterestType)
                               .Where(credit => credit.AccountNumber == accountNumber)
                               .ToListAsync();
    }

    public async Task<Credit?> FindByCreditNumber(int creditNumber)
    {
        return await m_Context.Credits.Include(credit => credit.CreditType)
                               .Include(credit => credit.Account)
                               .Include(credit => credit.Currency)
                               .Include(credit => credit.InterestType)
                               .FirstOrDefaultAsync(credit => credit.CreditNumber == creditNumber);
    }

    public async Task<Credit> Add(Credit credit)
    {
        var addedCredit = await m_Context.Credits.AddAsync(credit);
        await m_Context.SaveChangesAsync();

        var result = await m_Context.Credits.Include(c => c.Account)
                                     .Include(c => c.CreditType)
                                     .Include(c => c.Currency)
                                     .Include(c => c.InterestType)
                                     .FirstOrDefaultAsync(c => c.Id == credit.Id);

        return result!;
    }

    public async Task<Credit> Update(Credit oldCredit, Credit credit)
    {
        m_Context.Credits.Entry(oldCredit)
                  .State = EntityState.Detached;

        var updatedCredit = m_Context.Credits.Update(credit);
        await m_Context.SaveChangesAsync();
        return updatedCredit.Entity;
    }

    public async Task<Credit> UpdateStatus(Guid id, string status)
    {
        var credit = await FindById(id);

        if (credit == null)
            throw new Exception("Credit not found.");

        credit.CreditStatus = status;

        await m_Context.SaveChangesAsync();
        return credit;
    }

    public async Task<Credit> ApproveCreditRequest(Guid id)
    {
        // TODO: Prebaciti u servis
        var credit = await FindById(id);

        if (credit == null)
            throw new Exception("Credit request not found.");
        
        if (credit.CreditStatus != "Pending")
            throw new Exception("Credit request is not in pending status.");

        credit.CreditStatus = "Approved";
        
        await m_Context.SaveChangesAsync();
        return credit;
    }

    public async Task<Credit> RejectCreditRequest(Guid id)
    {
        var credit = await FindById(id);

        if (credit == null)
            throw new Exception("Credit request not found.");
        
        if (credit.CreditStatus != "Pending")
            throw new Exception("Credit request is not in pending status.");

        credit.CreditStatus = "Rejected";

        await m_Context.SaveChangesAsync();
        return credit;
    }
}