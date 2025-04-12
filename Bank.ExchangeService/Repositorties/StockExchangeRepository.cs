using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.ExchangeService.Database;
using Bank.ExchangeService.Model;

using Microsoft.EntityFrameworkCore;

namespace Bank.ExchangeService.Repositorties;

public interface IStockExchangeRepository
{
    Task<StockExchange?> FindById(Guid id);

    Task<StockExchange> Add(StockExchange stockExchange);

    Task<StockExchange> Update(StockExchange stockExchange);

    Task<Page<StockExchange>> FindAll(StockExchangeFilterQuery filter, Pageable pageable);
}

public class StockExchangeRepository(DatabaseContext context) : IStockExchangeRepository
{
    private readonly DatabaseContext m_Context = context;

    public async Task<Page<StockExchange>> FindAll(StockExchangeFilterQuery filter, Pageable pageable)
    {
        var query = m_Context.StockExchanges.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(e => EF.Functions.ILike(e.Name, $"%{filter.Name}%"));

        if (!string.IsNullOrEmpty(filter.Acronym))
            query = query.Where(e => EF.Functions.ILike(e.Acronym, $"%{filter.Acronym}%"));

        if (!string.IsNullOrEmpty(filter.MIC))
            query = query.Where(e => EF.Functions.ILike(e.MIC, $"%{filter.MIC}%"));

        if (!string.IsNullOrEmpty(filter.Polity))
            query = query.Where(e => EF.Functions.ILike(e.Polity, $"%{filter.Polity}%"));

        var total = await query.CountAsync();

        var items = await query.Skip((pageable.Page - 1) * pageable.Size)
                               .Take(pageable.Size)
                               .ToListAsync();

        return new Page<StockExchange>(items, pageable.Page, pageable.Size, total);
    }

    public async Task<StockExchange?> FindById(Guid id)
    {
        return await m_Context.StockExchanges.FindAsync(id);
    }

    public async Task<StockExchange> Add(StockExchange stockExchange)
    {
        var added = await m_Context.StockExchanges.AddAsync(stockExchange);
        await m_Context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<StockExchange> Update(StockExchange stockExchange)
    {
        m_Context.StockExchanges.Update(stockExchange);
        await m_Context.SaveChangesAsync();
        return stockExchange;
    }
}
