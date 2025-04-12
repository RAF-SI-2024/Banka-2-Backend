using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class ExchangeEntityConfiguration : IEntityTypeConfiguration<Exchange>
{
    public void Configure(EntityTypeBuilder<Exchange> builder)
    {
        builder.HasKey(exchange => exchange.Id);
        builder.HasAlternateKey(nameof(Exchange.CurrencyFromId), nameof(Exchange.CurrencyToId), nameof(Exchange.CreatedAt));

        builder.Property(exchange => exchange.Id)
               .IsRequired()
               .ValueGeneratedOnAdd();

        builder.HasOne(exchange => exchange.CurrencyFrom)
               .WithMany()
               .HasForeignKey(exchange => exchange.CurrencyFromId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(exchange => exchange.CurrencyTo)
               .WithMany()
               .HasForeignKey(exchange => exchange.CurrencyToId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(exchange => exchange.Commission)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(exchange => exchange.Rate)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(exchange => exchange.CreatedAt)
               .IsRequired();

        builder.Property(exchange => exchange.ModifiedAt)
               .IsRequired();
    }
}
