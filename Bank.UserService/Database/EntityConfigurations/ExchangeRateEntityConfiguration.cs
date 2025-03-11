using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class ExchangeRateEntityConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(exchangeRate => exchangeRate.Id);
        builder.HasAlternateKey(nameof(ExchangeRate.CurrencyFromId), nameof(ExchangeRate.CurrencyToId));

        builder.Property(exchangeRate => exchangeRate.Id)
               .IsRequired()
               .ValueGeneratedOnAdd();

        builder.HasOne(exchangeRate => exchangeRate.CurrencyFrom)
               .WithMany()
               .HasForeignKey(exchangeRate => exchangeRate.CurrencyFromId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(exchangeRate => exchangeRate.CurrencyTo)
               .WithMany()
               .HasForeignKey(exchangeRate => exchangeRate.CurrencyToId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(exchangeRate => exchangeRate.Commission)
               .IsRequired();

        builder.Property(exchangeRate => exchangeRate.Rate)
               .IsRequired();

        builder.Property(exchangeRate => exchangeRate.CreatedAt)
               .IsRequired();

        builder.Property(exchangeRate => exchangeRate.ModifiedAt)
               .IsRequired();
    }
}
