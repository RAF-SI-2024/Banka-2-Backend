using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface IExchangeRepository
{
    public Task<List<Exchange>> FindAll(ExchangeFilterQuery exchangeFilterQuery);

    public Task<Exchange?> FindById(Guid id);

    public Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Currency firstCurrency, Currency secondCurrency, ExchangeFilterQuery exchangeFilterQuery);

    public Task<Exchange?> FindByCurrencyFromIdAndCurrencyToId(Guid firstCurrencyId, Guid secondCurrencyId, ExchangeFilterQuery exchangeFilterQuery);

    public Task<Exchange?> FindByCurrencyFromCodeAndCurrencyToCode(string firstCurrencyCode, string secondCurrencyCode, ExchangeFilterQuery exchangeFilterQuery);

    public Task<Exchange> Add(Exchange exchange);

    public Task<Exchange> Update(Exchange oldExchange, Exchange exchange);
}

public class ExchangeRepository(ApplicationContext context) : IExchangeRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<List<Exchange>> FindAll(ExchangeFilterQuery exchangeFilterQuery)
    {
        var exchangeQueue = m_Context.Exchanges.Include(exchange => exchange.CurrencyFrom)
                                     .Include(exchange => exchange.CurrencyTo)
                                     .AsQueryable();

        if (!string.IsNullOrEmpty(exchangeFilterQuery.CurrencyCode))
            exchangeQueue = exchangeQueue.Where(exchange => exchange.CurrencyFrom.Code.ToLower() == exchangeFilterQuery.CurrencyCode.ToLower() ||
                                                            exchange.CurrencyTo.Code.ToLower()   == exchangeFilterQuery.CurrencyCode.ToLower());

        if (exchangeFilterQuery.CurrencyId != Guid.Empty)
            exchangeQueue = exchangeQueue.Where(exchange => exchange.CurrencyFromId == exchangeFilterQuery.CurrencyId || exchange.CurrencyToId == exchangeFilterQuery.CurrencyId);

        if (exchangeFilterQuery.Date == DateOnly.MinValue)
            exchangeFilterQuery.Date = DateOnly.FromDateTime(DateTime.UtcNow);

        exchangeQueue = exchangeQueue.Where(exchange => exchangeFilterQuery.Date                  <= DateOnly.FromDateTime(exchange.CreatedAt) &&
                                                        DateOnly.FromDateTime(exchange.CreatedAt) < exchangeFilterQuery.Date.AddDays(1));

        return await exchangeQueue.ToListAsync();
    }

    public async Task<Exchange?> FindById(Guid id)
    {
        return await m_Context.Exchanges.Include(exchange => exchange.CurrencyFrom)
                              .Include(exchange => exchange.CurrencyTo)
                              .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Exchange?> FindByCurrencyFromAndCurrencyTo(Currency firstCurrency, Currency secondCurrency, ExchangeFilterQuery exchangeFilterQuery)
    {
        return await FindByCurrencyFromIdAndCurrencyToId(firstCurrency.Id, secondCurrency.Id, exchangeFilterQuery);
    }

    public async Task<Exchange?> FindByCurrencyFromIdAndCurrencyToId(Guid firstCurrencyId, Guid secondCurrencyId, ExchangeFilterQuery exchangeFilterQuery)
    {
        if (exchangeFilterQuery.Date == DateOnly.MinValue)
            exchangeFilterQuery.Date = DateOnly.FromDateTime(DateTime.UtcNow);

        return await m_Context.Exchanges.Include(exchange => exchange.CurrencyFrom)
                              .Include(exchange => exchange.CurrencyTo)
                              .FirstOrDefaultAsync(exchange => ((exchange.CurrencyFromId == firstCurrencyId  && exchange.CurrencyToId == secondCurrencyId) ||
                                                                (exchange.CurrencyFromId == secondCurrencyId && exchange.CurrencyToId == firstCurrencyId)) &&
                                                               exchangeFilterQuery.Date                  <= DateOnly.FromDateTime(exchange.CreatedAt)      &&
                                                               DateOnly.FromDateTime(exchange.CreatedAt) < exchangeFilterQuery.Date.AddDays(1));
    }

    public async Task<Exchange?> FindByCurrencyFromCodeAndCurrencyToCode(string firstCurrencyCode, string secondCurrencyCode, ExchangeFilterQuery exchangeFilterQuery)
    {
        if (exchangeFilterQuery.Date == DateOnly.MinValue)
            exchangeFilterQuery.Date = DateOnly.FromDateTime(DateTime.UtcNow);

        return await m_Context.Exchanges.Include(exchange => exchange.CurrencyFrom)
                              .Include(exchange => exchange.CurrencyTo)
                              .FirstOrDefaultAsync(exchange => ((exchange.CurrencyFrom.Code == firstCurrencyCode  && exchange.CurrencyTo.Code == secondCurrencyCode) ||
                                                                (exchange.CurrencyFrom.Code == secondCurrencyCode && exchange.CurrencyTo.Code == firstCurrencyCode)) &&
                                                               exchangeFilterQuery.Date                  <= DateOnly.FromDateTime(exchange.CreatedAt)                &&
                                                               DateOnly.FromDateTime(exchange.CreatedAt) < exchangeFilterQuery.Date.AddDays(1));
    }

    public async Task<Exchange> Add(Exchange exchange)
    {
        var addExchange = await m_Context.Exchanges.AddAsync(exchange);

        await m_Context.SaveChangesAsync();

        return addExchange.Entity;
    }

    public async Task<Exchange> Update(Exchange oldExchange, Exchange exchange)
    {
        m_Context.Exchanges.Entry(oldExchange)
                 .State = EntityState.Detached;

        var newExchange = m_Context.Exchanges.Update(exchange);

        await m_Context.SaveChangesAsync();

        return newExchange.Entity;
    }
}
