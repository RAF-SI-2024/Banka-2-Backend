using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Database.Core;
using Bank.ExchangeService.Model;

using Microsoft.EntityFrameworkCore;

using DatabaseContext = Bank.ExchangeService.Database.DatabaseContext;

namespace Bank.ExchangeService.Repositories;

public interface IStockExchangeRepository
{
    Task<StockExchange?> FindById(Guid id);

    Task<StockExchange?> FindByAcronym(string acronym);

    Task<StockExchange> Add(StockExchange stockExchange);

    Task<StockExchange> Update(StockExchange stockExchange);

    Task<Page<StockExchange>> FindAll(StockExchangeFilterQuery filter, Pageable pageable);
}

public class StockExchangeRepository(IDatabaseContextFactory<DatabaseContext> contextFactory) : IStockExchangeRepository
{
    private readonly IDatabaseContextFactory<DatabaseContext> m_ContextFactory = contextFactory;

    public async Task<Page<StockExchange>> FindAll(StockExchangeFilterQuery filter, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var query = context.StockExchanges.AsQueryable();

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
        await using var context = await m_ContextFactory.CreateContext;

        return await context.StockExchanges.FindAsync(id);
    }

    public async Task<StockExchange?> FindByAcronym(string acronym)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.StockExchanges.Where(stockExchange => EF.Functions.ILike(stockExchange.Acronym, $"%{acronym}%"))
                            .FirstAsync();
    }

    public async Task<StockExchange> Add(StockExchange stockExchange)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var added = await context.StockExchanges.AddAsync(stockExchange);

        await context.SaveChangesAsync();

        return added.Entity;
    }

    public async Task<StockExchange> Update(StockExchange stockExchange)
    {
        await using var context = await m_ContextFactory.CreateContext;

        context.StockExchanges.Update(stockExchange);

        await context.SaveChangesAsync();

        return stockExchange;
    }
}
