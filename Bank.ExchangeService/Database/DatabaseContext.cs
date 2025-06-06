using Bank.ExchangeService.Database.EntityConfigurations;
using Bank.ExchangeService.Model;
using Bank.ExchangeService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.ExchangeService.Database;

using DatabaseContextBase = Bank.Database.Core.DatabaseContext;

public class DatabaseContext(DbContextOptions options) : DatabaseContextBase(options)
{
    public DbSet<StockExchange> StockExchanges { init; get; }
    public DbSet<Quote>         Quotes         { init; get; }
    public DbSet<Security>      Securities     { init; get; }
    public DbSet<Order>         Orders         { init; get; }
    public DbSet<Asset>         Assets         { init; get; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new StockExchangeEntityConfiguration());
        builder.ApplyConfiguration(new QuoteEntityConfiguration());
        builder.ApplyConfiguration(new SecurityEntityConfiguration());
        builder.ApplyConfiguration(new OrderEntityConfiguration());
        builder.ApplyConfiguration(new AssetEntityConfiguration());
    }
}
