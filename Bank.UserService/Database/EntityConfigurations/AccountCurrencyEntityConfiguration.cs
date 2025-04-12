using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class AccountCurrencyEntityConfiguration : IEntityTypeConfiguration<AccountCurrency>
{
    public void Configure(EntityTypeBuilder<AccountCurrency> builder)
    {
        builder.HasKey(accountCurrency => accountCurrency.Id);

        builder.Property(accountCurrency => accountCurrency.Id)
               .IsRequired();

        builder.Property(accountCurrency => accountCurrency.AccountId)
               .IsRequired();

        builder.Property(accountCurrency => accountCurrency.CurrencyId)
               .IsRequired();

        builder.Property(accountCurrency => accountCurrency.EmployeeId)
               .IsRequired();

        builder.Property(accountCurrency => accountCurrency.Balance)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(accountCurrency => accountCurrency.AvailableBalance)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(accountCurrency => accountCurrency.DailyLimit)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(accountCurrency => accountCurrency.MonthlyLimit)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(accountCurrency => accountCurrency.CreatedAt)
               .IsRequired();

        builder.Property(accountCurrency => accountCurrency.ModifiedAt)
               .IsRequired();

        builder.HasOne(accountCurrency => accountCurrency.Account)
               .WithMany(account => account.AccountCurrencies)
               .HasForeignKey(accountCurrency => accountCurrency.AccountId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(accountCurrency => accountCurrency.Employee)
               .WithMany()
               .HasForeignKey(accountCurrency => accountCurrency.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(accountCurrency => accountCurrency.Currency)
               .WithMany()
               .HasForeignKey(accountCurrency => accountCurrency.CurrencyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
