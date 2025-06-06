using Bank.UserService.Database.ValueConverters;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
               .IsRequired();

        builder.Property(user => user.FirstName)
               .HasMaxLength(32)
               .IsRequired();

        builder.Property(user => user.LastName)
               .HasMaxLength(32)
               .IsRequired();

        builder.Property(user => user.DateOfBirth)
               .IsRequired();

        builder.Property(user => user.Gender)
               .IsRequired();

        builder.Property(user => user.UniqueIdentificationNumber)
               .HasMaxLength(13)
               .IsRequired();

        builder.Property(user => user.Email)
               .HasMaxLength(320)
               .IsRequired();

        builder.Property(user => user.Username)
               .HasMaxLength(32)
               .IsRequired(false);

        builder.Property(user => user.PhoneNumber)
               .HasMaxLength(13)
               .IsRequired();

        builder.Property(user => user.Address)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(user => user.Password)
               .HasMaxLength(64)
               .IsFixedLength()
               .IsRequired(false);

        builder.Property(user => user.Salt)
               .IsRequired();

        builder.Property(user => user.Role)
               .IsRequired();

        builder.Property(user => user.Department)
               .HasMaxLength(64)
               .IsRequired(false);

        builder.Property(user => user.CreatedAt)
               .IsRequired();

        builder.Property(user => user.ModifiedAt)
               .IsRequired();

        builder.Property(user => user.Employed)
               .IsRequired(false);

        builder.Property(user => user.Activated)
               .IsRequired();

        builder.Property(user => user.Permissions)
               .HasConversion(new PermissionsValueConverter())
               .IsRequired();

        builder.HasMany(user => user.Accounts)
               .WithOne(account => account.Client)
               .HasForeignKey(account => account.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.TransactionTemplates)
               .WithOne(transactionTemplate => transactionTemplate.Client)
               .HasForeignKey(transactionTemplate => transactionTemplate.ClientId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
