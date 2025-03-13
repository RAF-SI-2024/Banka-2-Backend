using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface ICurrencyRepository
{
    Task<Page<Currency>> FindAll(CurrencyFilterQuery currencyFilterQuery, Pageable pageable);

    Task<Currency?> FindById(Guid id);

    Task<Currency?> FindByCode(string currencyCode);

    Task<Currency> Add(Currency currency);

    Task<Currency> Update(Currency oldCurrency, Currency currency);
}

public class CurrencyRepository(ApplicationContext context) : ICurrencyRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Currency>> FindAll(CurrencyFilterQuery currencyFilterQuery, Pageable pageable)
    {
        var currencyQuery = m_Context.Currencies.Include(c => c.Countries)
                                     .AsQueryable();

        if (!string.IsNullOrEmpty(currencyFilterQuery.Name))
            currencyQuery = currencyQuery.Where(currency => EF.Functions.ILike(currency.Name, $"%{currencyFilterQuery.Name}%"));

        if (!string.IsNullOrEmpty(currencyFilterQuery.Code))
            currencyQuery = currencyQuery.Where(currency => EF.Functions.ILike(currency.Code, $"%{currencyFilterQuery.Code}%"));

        int totalElements = await currencyQuery.CountAsync();

        var currencies = await currencyQuery.Skip((pageable.Page - 1) * pageable.Size)
                                            .Take(pageable.Size)
                                            .ToListAsync();

        return new Page<Currency>(currencies, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Currency?> FindById(Guid id)
    {
        return await m_Context.Currencies.Include(c => c.Countries)
                              .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Currency?> FindByCode(string currencyCode)
    {
        return await m_Context.Currencies.Include(c => c.Countries)
                              .FirstOrDefaultAsync(x => x.Code == currencyCode);
    }

    public async Task<Currency> Add(Currency currency)
    {
        var addedCurrency = await m_Context.Currencies.AddAsync(currency);

        await m_Context.SaveChangesAsync();

        return addedCurrency.Entity;
    }

    public async Task<Currency> Update(Currency oldCurrency, Currency currency)
    {
        m_Context.Currencies.Entry(oldCurrency)
                 .State = EntityState.Detached;

        var updatedCurrency = m_Context.Currencies.Update(currency);

        await m_Context.SaveChangesAsync();

        return updatedCurrency.Entity;
    }
}
