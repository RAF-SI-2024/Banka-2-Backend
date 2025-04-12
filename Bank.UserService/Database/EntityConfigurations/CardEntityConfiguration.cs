using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class CardEntityConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(card => card.Id);

        builder.Property(card => card.Id)
               .IsRequired();

        builder.Property(card => card.Number)
               .HasMaxLength(16)
               .IsFixedLength()
               .IsRequired();

        // Ensure Card number is Unique
        builder.HasIndex(card => card.Number)
               .IsUnique();

        builder.HasOne(card => card.Type)
               .WithMany()
               .HasForeignKey(card => card.TypeId)
               .IsRequired();

        builder.Property(card => card.Name)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(card => card.ExpiresAt)
               .IsRequired();

        builder.HasOne(card => card.Account)
               .WithMany()
               .HasForeignKey(card => card.AccountId)
               .IsRequired();

        builder.Property(card => card.CVV)
               .HasMaxLength(4)
               .IsRequired();

        builder.Property(card => card.Limit)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(card => card.Status)
               .IsRequired();

        builder.Property(card => card.CreatedAt)
               .IsRequired();

        builder.Property(card => card.ModifiedAt)
               .IsRequired();
    }
}
