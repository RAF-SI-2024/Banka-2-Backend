using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface IAccountRepository
{
    Task<Page<Account>> FindAll(AccountFilterQuery accountFilterQuery, Pageable pageable);

    Task<Page<Account>> FindAllByClientId(Guid clientId, Pageable pageable);

    Task<Account?> FindById(Guid id);

    Task<Account> Add(Account account);

    Task<Account> Update(Account oldAccount, Account account);
}

public class AccountRepository(ApplicationContext context) : IAccountRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Account>> FindAll(AccountFilterQuery accountFilterQuery, Pageable pageable)
    {
        var accountQuery = m_Context.Accounts.Include(account => account.Client)
                                    .Include(account => account.Employee)
                                    .Include(account => account.Currency)
                                    .Include(account => account.AccountCurrencies)
                                    .Include(account => account.Type)
                                    .AsQueryable();

        if (!string.IsNullOrEmpty(accountFilterQuery.ClientEmail))
            accountQuery = accountQuery.Where(account => account.Client != null && EF.Functions.ILike(account.Client.Email, $"%{accountFilterQuery.ClientEmail}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.ClientFirstName))
            accountQuery = accountQuery.Where(account => account.Client != null && EF.Functions.ILike(account.Client.FirstName, $"%{accountFilterQuery.ClientFirstName}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.ClientLastName))
            accountQuery = accountQuery.Where(account => account.Client != null && EF.Functions.ILike(account.Client.LastName, $"%{accountFilterQuery.ClientLastName}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.AccountTypeName))
            accountQuery = accountQuery.Where(account => account.Type != null && EF.Functions.ILike(account.Type.Name, $"%{accountFilterQuery.AccountTypeName}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.CurrencyName))
            accountQuery = accountQuery.Where(account => account.Currency != null && EF.Functions.ILike(account.Currency.Name, $"%{accountFilterQuery.CurrencyName}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.EmployeeEmail))
            accountQuery = accountQuery.Where(account => account.Employee != null && EF.Functions.ILike(account.Employee.Email, $"%{accountFilterQuery.EmployeeEmail}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.Number))
            accountQuery = accountQuery.Where(account => EF.Functions.ILike(account.Number, $"%{accountFilterQuery.Number}%"));

        if (accountFilterQuery.Status != null)
            accountQuery = accountQuery.Where(account => account.Status == accountFilterQuery.Status.Value);

        var accounts = await accountQuery.Skip((pageable.Page - 1) * pageable.Size)
                                         .Take(pageable.Size)
                                         .ToListAsync();

        var totalElements = await accountQuery.CountAsync();

        return new Page<Account>(accounts, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Page<Account>> FindAllByClientId(Guid clientId, Pageable pageable)
    {
        var accounts = await m_Context.Accounts.Include(account => account.Client)
                                      .Include(account => account.Employee)
                                      .Include(account => account.Currency)
                                      .Include(account => account.AccountCurrencies)
                                      .Include(account => account.Type)
                                      .Where(account => account.ClientId == clientId)
                                      .Skip((pageable.Page - 1) * pageable.Size)
                                      .Take(pageable.Size)
                                      .ToListAsync();

        var totalElements = await m_Context.Accounts.CountAsync();

        return new Page<Account>(accounts, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Account?> FindById(Guid id)
    {
        return await m_Context.Accounts.Include(account => account.Client)
                              .Include(account => account.Employee)
                              .Include(account => account.Currency)
                              .Include(account => account.AccountCurrencies)
                              .Include(account => account.Type)
                              .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Account> Add(Account account)
    {
        var addedAccount = await m_Context.Accounts.AddAsync(account);

        await m_Context.SaveChangesAsync();

        return addedAccount.Entity;
    }

    public async Task<Account> Update(Account oldAccount, Account account)
    {
        m_Context.Accounts.Entry(oldAccount)
                 .State = EntityState.Detached;

        var updatedAccount = m_Context.Accounts.Update(account);

        await m_Context.SaveChangesAsync();

        return updatedAccount.Entity;
    }
}
