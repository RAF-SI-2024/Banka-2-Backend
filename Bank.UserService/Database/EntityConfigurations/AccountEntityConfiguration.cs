using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(account => account.Id);

        builder.Property(account => account.Id)
               .IsRequired();

        builder.Property(account => account.Name)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(account => account.Number)
               .HasMaxLength(9)
               .IsFixedLength()
               .IsRequired();

        builder.Property(account => account.Office)
               .HasMaxLength(4)
               .IsFixedLength()
               .IsRequired();

        builder.Property(account => account.ClientId)
               .IsRequired();

        builder.Property(account => account.Balance)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(account => account.AvailableBalance)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(account => account.EmployeeId)
               .IsRequired();

        builder.Property(account => account.CurrencyId)
               .IsRequired();

        builder.Property(account => account.AccountTypeId)
               .IsRequired();

        builder.Property(account => account.DailyLimit)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(account => account.MonthlyLimit)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(account => account.Status)
               .IsRequired();

        builder.Property(account => account.CreationDate)
               .IsRequired();

        builder.Property(account => account.ExpirationDate)
               .IsRequired();

        builder.Property(account => account.CreatedAt)
               .IsRequired();

        builder.Property(account => account.ModifiedAt)
               .IsRequired();

        builder.HasOne(account => account.Client)
               .WithMany(user => user.Accounts)
               .HasForeignKey(account => account.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(account => account.Employee)
               .WithMany()
               .HasForeignKey(account => account.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(account => account.Currency)
               .WithMany()
               .HasForeignKey(account => account.CurrencyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(account => account.Type)
               .WithMany()
               .HasForeignKey(account => account.AccountTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(account => account.AccountCurrencies)
               .WithOne(accountCurrency => accountCurrency.Account)
               .HasForeignKey(accountCurrency => accountCurrency.AccountId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
