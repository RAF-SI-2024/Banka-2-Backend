using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(country => country.Id);

        builder.Property(country => country.Id)
               .IsRequired()
               .ValueGeneratedOnAdd();

        builder.Property(country => country.Name)
               .IsRequired()
               .HasMaxLength(64);

        builder.HasOne(country => country.Currency)
               .WithMany(currency => currency.Countries)
               .HasForeignKey(country => country.CurrencyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(country => country.CreatedAt)
               .IsRequired();

        builder.Property(country => country.ModifiedAt)
               .IsRequired();
    }
}
