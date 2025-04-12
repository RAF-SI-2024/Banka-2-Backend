using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.ExchangeService.Database;
using Bank.ExchangeService.Model;

using Microsoft.EntityFrameworkCore;

namespace Bank.ExchangeService.Repositorties;

public interface IListingRepository
{
    Task<Listing?> FindById(Guid id);

    Task<Listing> Add(Listing listing);

    Task<Listing> Update(Listing listing);

    Task<List<Listing>> FindByExchangeId(Guid exchangeId);

    Task<Page<Listing>> FindAll(ListingFilterQuery filter, Pageable pageable);
}

public class ListingRepository(DatabaseContext context) : IListingRepository
{
    private readonly DatabaseContext m_Context = context;

    public async Task<Page<Listing>> FindAll(ListingFilterQuery filter, Pageable pageable)
    {
        var query = m_Context.Listings.Include(l => l.StockExchange)
                             .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(l => EF.Functions.ILike(l.Name, $"%{filter.Name}%"));

        if (!string.IsNullOrEmpty(filter.Ticker))
            query = query.Where(l => EF.Functions.ILike(l.Ticker, $"%{filter.Ticker}%"));

        if (filter.ExchangeId.HasValue)
            query = query.Where(l => l.StockExchangeId == filter.ExchangeId.Value);

        var total = await query.CountAsync();

        var items = await query.Skip((pageable.Page - 1) * pageable.Size)
                               .Take(pageable.Size)
                               .ToListAsync();

        return new Page<Listing>(items, pageable.Page, pageable.Size, total);
    }

    public async Task<Listing?> FindById(Guid id)
    {
        return await m_Context.Listings.Include(l => l.StockExchange)
                              .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Listing> Add(Listing listing)
    {
        var added = await m_Context.Listings.AddAsync(listing);
        await m_Context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Listing> Update(Listing listing)
    {
        m_Context.Listings.Update(listing);
        await m_Context.SaveChangesAsync();
        return listing;
    }

    public async Task<List<Listing>> FindByExchangeId(Guid exchangeId)
    {
        return await m_Context.Listings.Where(l => l.StockExchangeId == exchangeId)
                              .ToListAsync();
    }
}
