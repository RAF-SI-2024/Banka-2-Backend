using Bank.ExchangeService.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.ExchangeService.Database.EntityConfigurations;

public class ListingHistoricalEntityConfiguration : IEntityTypeConfiguration<ListingHistorical>
{
    public void Configure(EntityTypeBuilder<ListingHistorical> builder)
    {
        builder.HasKey(history => history.Id);

        builder.Property(history => history.Id)
               .IsRequired();

        builder.Property(history => history.ListingId);

        builder.Property(history => history.ClosingPrice)
               .IsRequired()
               .HasPrecision(18, 6);

        builder.Property(history => history.HighPrice)
               .IsRequired()
               .HasPrecision(18, 6);

        builder.Property(history => history.LowPrice)
               .IsRequired()
               .HasPrecision(18, 6);

        builder.Property(history => history.PriceChange)
               .IsRequired()
               .HasPrecision(18, 6);

        builder.Property(history => history.Volume)
               .IsRequired();

        builder.Property(history => history.CreatedAt)
               .IsRequired();

        builder.Property(history => history.ModifiedAt)
               .IsRequired();

        builder.HasOne(history => history.Listing) // Use the navigation property name
               .WithMany()
               .HasForeignKey(history => history.ListingId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
