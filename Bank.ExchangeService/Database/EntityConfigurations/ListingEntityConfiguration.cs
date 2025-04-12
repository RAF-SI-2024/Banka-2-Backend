using Bank.ExchangeService.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ListingEntityConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.HasKey(listing => listing.Id);

        builder.Property(listing => listing.Id)
               .IsRequired();

        builder.Property(listing => listing.Name)
               .IsRequired()
               .HasMaxLength(128);

        builder.Property(listing => listing.Ticker)
               .IsRequired()
               .HasMaxLength(16);

        builder.Property(listing => listing.StockExchangeId)
               .IsRequired();

        builder.Property(listing => listing.CreatedAt)
               .IsRequired();

        builder.Property(listing => listing.ModifiedAt)
               .IsRequired();

        // Fix the relationship mapping by explicitly naming the navigation property and foreign key
        builder.HasOne(listing => listing.StockExchange)
               .WithMany()
               .HasForeignKey(listing => listing.StockExchangeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
