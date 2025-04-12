using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class LoanEntityConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(loan => loan.Id);

        builder.Property(loan => loan.Id)
               .IsRequired();

        builder.Property(loan => loan.TypeId)
               .IsRequired();

        builder.Property(loan => loan.AccountId)
               .IsRequired();

        builder.Property(loan => loan.Amount)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(loan => loan.Period)
               .IsRequired();

        builder.Property(loan => loan.CreationDate)
               .IsRequired();

        builder.Property(loan => loan.MaturityDate)
               .IsRequired();

        builder.Property(loan => loan.CurrencyId)
               .IsRequired();

        builder.Property(loan => loan.Status)
               .IsRequired();

        builder.Property(loan => loan.InterestType)
               .IsRequired();

        builder.Property(loan => loan.CreatedAt)
               .IsRequired();

        builder.Property(loan => loan.ModifiedAt)
               .IsRequired();

        builder.HasOne(loan => loan.LoanType)
               .WithMany()
               .HasForeignKey(loan => loan.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(loan => loan.Account)
               .WithMany()
               .HasForeignKey(loan => loan.AccountId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(loan => loan.Currency)
               .WithMany()
               .HasForeignKey(loan => loan.CurrencyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(loan => loan.Installments)
               .WithOne(installment => installment.Loan)
               .HasForeignKey(installment => installment.LoanId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
