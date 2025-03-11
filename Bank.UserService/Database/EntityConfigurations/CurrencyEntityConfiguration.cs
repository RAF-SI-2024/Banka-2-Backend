using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class CurrencyEntityConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(currency => currency.Id);

        builder.Property(currency => currency.Id)
               .IsRequired()
               .ValueGeneratedOnAdd();

        builder.Property(currency => currency.Name)
               .IsRequired()
               .HasMaxLength(64);

        builder.Property(currency => currency.Code)
               .IsRequired()
               .HasMaxLength(3)
               .IsFixedLength();

        builder.Property(currency => currency.Symbol)
               .IsRequired()
               .HasMaxLength(3);

        builder.HasMany(currency => currency.Countries)
               .WithOne(country => country.Currency)
               .HasForeignKey(country => country.CurrencyId);

        builder.Property(currency => currency.Description)
               .IsRequired()
               .HasMaxLength(1_000);

        builder.Property(currency => currency.Status)
               .IsRequired();

        builder.Property(currency => currency.CreatedAt)
               .IsRequired();

        builder.Property(currency => currency.ModifiedAt)
               .IsRequired();
    }
}
