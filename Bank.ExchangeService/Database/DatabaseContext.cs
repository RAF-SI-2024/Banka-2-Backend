using Bank.ExchangeService.Database.EntityConfigurations;
using Bank.ExchangeService.Model;

using Microsoft.EntityFrameworkCore;

namespace Bank.ExchangeService.Database;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<StockExchange>     StockExchanges     { init; get; }
    public DbSet<Listing>           Listings           { init; get; }
    public DbSet<ListingHistorical> ListingHistoricals { init; get; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new StockExchangeEntityConfiguration());
        builder.ApplyConfiguration(new ListingEntityConfiguration());
        builder.ApplyConfiguration(new ListingHistoricalEntityConfiguration());
    }
}
