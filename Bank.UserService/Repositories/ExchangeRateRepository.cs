using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface IExchangeRateRepository
{
    public Task<List<ExchangeRate>> FindAll(ExchangeRateFilterQuery exchangeRateFilterQuery);

    public Task<ExchangeRate?> FindById(Guid id);

    public Task<ExchangeRate?> FindByCurrencyFromAndCurrencyTo(Currency firstCurrency, Currency secondCurrency);

    public Task<ExchangeRate?> FindByCurrencyFromIdAndCurrencyToId(Guid firstCurrencyId, Guid secondCurrencyId);

    public Task<ExchangeRate?> FindByCurrencyFromCodeAndCurrencyToCode(string firstCurrencyCode, string secondCurrencyCode);

    public Task<ExchangeRate> Add(ExchangeRate exchangeRate);

    public Task<ExchangeRate> Update(ExchangeRate oldExchangeRate, ExchangeRate exchangeRate);
}

public class ExchangeRateRepository(ApplicationContext context) : IExchangeRateRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<List<ExchangeRate>> FindAll(ExchangeRateFilterQuery exchangeRateFilterQuery)
    {
        var exchangeRateQueue = m_Context.ExchangeRates.Include(exchangeRate => exchangeRate.CurrencyFrom)
                                         .Include(exchangeRate => exchangeRate.CurrencyTo)
                                         .AsQueryable();

        if (!string.IsNullOrEmpty(exchangeRateFilterQuery.CurrencyCode))
            exchangeRateQueue = exchangeRateQueue.Where(exchangeRate => exchangeRate.CurrencyFrom.Code.ToLower() == exchangeRateFilterQuery.CurrencyCode.ToLower() ||
                                                                        exchangeRate.CurrencyTo.Code.ToLower()   == exchangeRateFilterQuery.CurrencyCode.ToLower());

        if (exchangeRateFilterQuery.CurrencyId is not null)
            exchangeRateQueue = exchangeRateQueue.Where(exchangeRate => exchangeRate.CurrencyFromId == exchangeRateFilterQuery.CurrencyId ||
                                                                        exchangeRate.CurrencyToId   == exchangeRateFilterQuery.CurrencyId);

        return await exchangeRateQueue.ToListAsync();
    }

    public async Task<ExchangeRate?> FindById(Guid id)
    {
        return await m_Context.ExchangeRates.Include(exchangeRate => exchangeRate.CurrencyFrom)
                              .Include(exchangeRate => exchangeRate.CurrencyTo)
                              .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ExchangeRate?> FindByCurrencyFromAndCurrencyTo(Currency firstCurrency, Currency secondCurrency)
    {
        return await FindByCurrencyFromIdAndCurrencyToId(firstCurrency.Id, secondCurrency.Id);
    }

    public async Task<ExchangeRate?> FindByCurrencyFromIdAndCurrencyToId(Guid firstCurrencyId, Guid secondCurrencyId)
    {
        return await m_Context.ExchangeRates.Include(exchangeRate => exchangeRate.CurrencyFrom)
                              .Include(exchangeRate => exchangeRate.CurrencyTo)
                              .FirstOrDefaultAsync(exchangeRate => (exchangeRate.CurrencyFromId == firstCurrencyId  && exchangeRate.CurrencyToId == secondCurrencyId) ||
                                                                   (exchangeRate.CurrencyFromId == secondCurrencyId && exchangeRate.CurrencyToId == firstCurrencyId));
    }

    public async Task<ExchangeRate?> FindByCurrencyFromCodeAndCurrencyToCode(string firstCurrencyCode, string secondCurrencyCode)
    {
        return await m_Context.ExchangeRates.Include(exchangeRate => exchangeRate.CurrencyFrom)
                              .Include(exchangeRate => exchangeRate.CurrencyTo)
                              .FirstOrDefaultAsync(exchangeRate => (exchangeRate.CurrencyFrom.Code == firstCurrencyCode  && exchangeRate.CurrencyTo.Code == secondCurrencyCode) ||
                                                                   (exchangeRate.CurrencyFrom.Code == secondCurrencyCode && exchangeRate.CurrencyTo.Code == firstCurrencyCode));
    }

    public async Task<ExchangeRate> Add(ExchangeRate exchangeRate)
    {
        var addExchangeRate = await m_Context.ExchangeRates.AddAsync(exchangeRate);

        await m_Context.SaveChangesAsync();

        return addExchangeRate.Entity;
    }

    public async Task<ExchangeRate> Update(ExchangeRate oldExchangeRate, ExchangeRate exchangeRate)
    {
        m_Context.ExchangeRates.Entry(oldExchangeRate)
                 .State = EntityState.Detached;

        var updatedExchangeRate = m_Context.ExchangeRates.Update(exchangeRate);

        await m_Context.SaveChangesAsync();

        return updatedExchangeRate.Entity;
    }
}
