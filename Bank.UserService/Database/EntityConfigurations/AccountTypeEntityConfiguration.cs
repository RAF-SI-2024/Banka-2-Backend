using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class AccountTypeEntityConfiguration : IEntityTypeConfiguration<AccountType>
{
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        builder.HasKey(accountType => accountType.Id);

        builder.Property(accountType => accountType.Id)
               .IsRequired();

        builder.Property(accountType => accountType.Name)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(accountType => accountType.Code)
               .HasMaxLength(2)
               .IsFixedLength()
               .IsRequired();

        builder.Property(accountType => accountType.CreatedAt)
               .IsRequired();

        builder.Property(accountType => accountType.ModifiedAt)
               .IsRequired();
    }
}
