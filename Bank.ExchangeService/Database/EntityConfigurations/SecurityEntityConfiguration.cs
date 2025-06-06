using Bank.ExchangeService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.ExchangeService.Database.EntityConfigurations;

public class SecurityEntityConfiguration : IEntityTypeConfiguration<Security>
{
    public void Configure(EntityTypeBuilder<Security> builder)
    {
        builder.HasKey(security => security.Id);

        builder.HasAlternateKey(nameof(Security.Ticker));

        builder.Property(security => security.Id)
               .IsRequired();

        builder.Property(security => security.Name)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(security => security.Ticker)
               .IsRequired()
               .HasMaxLength(32);

        builder.Property(security => security.ContractSize)
               .IsRequired();

        builder.Property(security => security.Liquidity)
               .IsRequired(false);

        builder.Property(security => security.ExchangeRate)
               .IsRequired();

        //Option
        builder.Property(security => security.OptionType)
               .IsRequired(false);

        builder.Property(security => security.StrikePrice)
               .IsRequired();

        builder.Property(security => security.OpenInterest)
               .IsRequired();

        builder.Property(security => security.SettlementDate)
               .IsRequired();

        //Future Contract
        builder.Property(security => security.ContractUnit)
               .IsRequired(false);

        //Foreign keys
        builder.Property(security => security.BaseCurrencyId)
               .IsRequired();

        builder.Property(security => security.QuoteCurrencyId)
               .IsRequired();

        builder.Property(security => security.StockExchangeId)
               .IsRequired();

        builder.HasOne(security => security.StockExchange)
               .WithMany()
               .HasForeignKey(security => security.StockExchangeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(security => security.Quotes)
               .WithOne(quote => quote.Security)
               .HasForeignKey(quote => quote.SecurityId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
