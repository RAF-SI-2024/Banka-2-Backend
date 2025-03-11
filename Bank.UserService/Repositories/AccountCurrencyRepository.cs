using Bank.Application.Domain;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface IAccountCurrencyRepository
{
    Task<Page<AccountCurrency>> FindAll(Pageable pageable); //todo: filter

    Task<AccountCurrency?> FindById(Guid id);

    Task<AccountCurrency> Add(AccountCurrency accountCurrency);

    Task<AccountCurrency> Update(AccountCurrency oldAccountTypeCurrency, AccountCurrency accountCurrency);
}

public class AccountCurrencyRepository(ApplicationContext context) : IAccountCurrencyRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<AccountCurrency>> FindAll(Pageable pageable)
    {
        var accountTypeQuery = m_Context.AccountCurrencies.Include(account => account.Employee)
                                        .Include(account => account.Currency)
                                        .Include(account => account.Account)
                                        .AsQueryable();

        var accountTypes = await accountTypeQuery.Skip((pageable.Page - 1) * pageable.Size)
                                                 .Take(pageable.Size)
                                                 .ToListAsync();

        var totalElements = await accountTypeQuery.CountAsync();

        return new Page<AccountCurrency>(accountTypes, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<AccountCurrency?> FindById(Guid id)
    {
        return await m_Context.AccountCurrencies.Include(account => account.Employee)
                              .Include(account => account.Currency)
                              .Include(account => account.Account)
                              .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<AccountCurrency> Add(AccountCurrency accountCurrency)
    {
        var addedAccountCurrency = await m_Context.AccountCurrencies.AddAsync(accountCurrency);

        await m_Context.SaveChangesAsync();

        return addedAccountCurrency.Entity;
    }

    public async Task<AccountCurrency> Update(AccountCurrency oldAccountCurrency, AccountCurrency accountCurrency)
    {
        m_Context.AccountCurrencies.Entry(oldAccountCurrency)
                 .State = EntityState.Detached;

        var updatedAccountCurrency = m_Context.AccountCurrencies.Update(accountCurrency);

        await m_Context.SaveChangesAsync();

        return updatedAccountCurrency.Entity;
    }
}
