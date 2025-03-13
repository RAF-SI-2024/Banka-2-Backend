using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class InstallmentEntityConfiguration : IEntityTypeConfiguration<Installment>
{
    public void Configure(EntityTypeBuilder<Installment> builder)
    {
        builder.HasKey(installment => installment.Id);

        builder.Property(installment => installment.Id)
               .IsRequired();

        builder.Property(installment => installment.LoanId)
               .IsRequired();

        builder.Property(installment => installment.InterestRate)
               .IsRequired()
               .HasPrecision(18, 6);

        builder.Property(installment => installment.ExpectedDueDate)
               .IsRequired();

        builder.Property(installment => installment.ActualDueDate)
               .IsRequired();

        builder.Property(installment => installment.Status)
               .IsRequired();

        builder.Property(installment => installment.CreatedAt)
               .IsRequired();

        builder.Property(installment => installment.ModifiedAt)
               .IsRequired();

        builder.HasOne(installment => installment.Loan)
               .WithMany(loan => loan.Installments)
               .HasForeignKey(installment => installment.LoanId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
