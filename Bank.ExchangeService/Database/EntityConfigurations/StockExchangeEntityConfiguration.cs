using Bank.ExchangeService.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StockExchangeEntityConfiguration : IEntityTypeConfiguration<StockExchange>
{
    public void Configure(EntityTypeBuilder<StockExchange> builder)
    {
        builder.HasKey(exchange => exchange.Id);

        builder.Property(exchange => exchange.Id)
               .IsRequired();

        builder.Property(exchange => exchange.Name)
               .IsRequired()
               .HasMaxLength(128);

        builder.Property(exchange => exchange.Acronym)
               .IsRequired()
               .HasMaxLength(16);

        builder.Property(exchange => exchange.MIC)
               .IsRequired(false) // Make MIC nullable
               .HasMaxLength(4);

        builder.Property(exchange => exchange.Polity)
               .IsRequired(false) // Make Polity nullable
               .HasMaxLength(64);

        builder.Property(exchange => exchange.CurrencyId);

        builder.Property(exchange => exchange.TimeZone);

        builder.Property(exchange => exchange.CreatedAt)
               .IsRequired();

        builder.Property(exchange => exchange.ModifiedAt)
               .IsRequired();
    }
}
