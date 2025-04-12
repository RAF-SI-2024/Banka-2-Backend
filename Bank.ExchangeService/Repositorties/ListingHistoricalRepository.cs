using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.ExchangeService.Database;
using Bank.ExchangeService.Model;

using Microsoft.EntityFrameworkCore;

namespace Bank.ExchangeService.Repositorties;

public interface IListingHistoricalRepository
{
    Task<ListingHistorical?> FindById(Guid id);

    Task<ListingHistorical> Add(ListingHistorical listingHistorical);

    Task<ListingHistorical> Update(ListingHistorical listingHistorical);

    Task<List<ListingHistorical>> FindByListingId(Guid listingId);

    Task<Page<ListingHistorical>> FindAll(ListingHistoricalFilterQuery filter, Pageable pageable);
}

public class ListingHistoricalRepository(DatabaseContext context) : IListingHistoricalRepository
{
    private readonly DatabaseContext m_Context = context;

    public async Task<Page<ListingHistorical>> FindAll(ListingHistoricalFilterQuery filter, Pageable pageable)
    {
        var query = m_Context.ListingHistoricals.Include(h => h.Listing)
                             .ThenInclude(l => l.StockExchange)
                             .AsQueryable();

        if (filter.ListingId.HasValue)
            query = query.Where(h => h.ListingId == filter.ListingId.Value);

        if (filter.FromDate.HasValue)
            query = query.Where(h => h.CreatedAt >= filter.FromDate.Value);

        if (filter.ToDate.HasValue)
            query = query.Where(h => h.CreatedAt <= filter.ToDate.Value);

        var total = await query.CountAsync();

        var items = await query.Skip((pageable.Page - 1) * pageable.Size)
                               .Take(pageable.Size)
                               .ToListAsync();

        return new Page<ListingHistorical>(items, pageable.Page, pageable.Size, total);
    }

    public async Task<ListingHistorical?> FindById(Guid id)
    {
        return await m_Context.ListingHistoricals.Include(h => h.Listing)
                              .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<ListingHistorical> Add(ListingHistorical listingHistorical)
    {
        var added = await m_Context.ListingHistoricals.AddAsync(listingHistorical);
        await m_Context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<ListingHistorical> Update(ListingHistorical listingHistorical)
    {
        m_Context.ListingHistoricals.Update(listingHistorical);
        await m_Context.SaveChangesAsync();
        return listingHistorical;
    }

    public async Task<List<ListingHistorical>> FindByListingId(Guid listingId)
    {
        return await m_Context.ListingHistoricals.Where(h => h.ListingId == listingId)
                              .ToListAsync();
    }
}
