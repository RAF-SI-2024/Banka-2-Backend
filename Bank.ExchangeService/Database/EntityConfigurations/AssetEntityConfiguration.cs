using Bank.ExchangeService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.ExchangeService.Database.EntityConfigurations;

public class AssetEntityConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(asset => asset.Id);

        builder.Property(asset => asset.Id)
               .IsRequired();

        builder.Property(asset => asset.ActuaryId)
               .IsRequired();

        builder.Property(asset => asset.SecurityId)
               .IsRequired();

        builder.Property(asset => asset.Quantity)
               .IsRequired();

        builder.Property(asset => asset.AveragePrice)
               .IsRequired();

        builder.Property(order => order.CreatedAt)
               .IsRequired();

        builder.Property(order => order.ModifiedAt)
               .IsRequired();

        builder.HasOne(asset => asset.Security)
               .WithMany()
               .HasForeignKey(asset => asset.SecurityId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
