using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class CreditInstallmentEntityConfiguration : IEntityTypeConfiguration<CreditInstallment>
{
    public void Configure(EntityTypeBuilder<CreditInstallment> builder)
    {
        builder.HasKey(installment => installment.Id);

        builder.Property(installment => installment.Id)
               .IsRequired();

        builder.Property(installment => installment.CreditNumber)
               .IsRequired();

        builder.Property(installment => installment.InstallmentAmount)
               .IsRequired();

        builder.Property(installment => installment.InterestRate)
               .IsRequired();

        builder.Property(installment => installment.CurrencyId)
               .IsRequired();

        builder.Property(installment => installment.ExpectedDueDate)
               .IsRequired();

        builder.Property(installment => installment.ActualDueDate);

        builder.Property(installment => installment.PaymentStatusId)
               .IsRequired();

        builder.Property(installment => installment.CreatedAt)
               .IsRequired();

        builder.Property(installment => installment.ModifiedAt)
               .IsRequired();

        builder.HasOne(installment => installment.Credit)
               .WithMany()
               .HasForeignKey(installment => installment.CreditNumber)
               .HasPrincipalKey(credit => credit.CreditNumber)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(installment => installment.Currency)
               .WithMany()
               .HasForeignKey(installment => installment.CurrencyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(installment => installment.PaymentStatus)
               .IsRequired();
    }
}
