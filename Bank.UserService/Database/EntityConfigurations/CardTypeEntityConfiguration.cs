using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class CardTypeEntityConfiguration : IEntityTypeConfiguration<CardType>
{
    public void Configure(EntityTypeBuilder<CardType> builder)
    {
        builder.HasKey(cardType => cardType.Id);

        builder.Property(cardType => cardType.Id)
               .IsRequired();

        builder.Property(cardType => cardType.Name)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(cardType => cardType.CreatedAt)
               .IsRequired();

        builder.Property(cardType => cardType.ModifiedAt)
               .IsRequired();
    }
}
