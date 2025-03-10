using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class CreditEntityConfiguration : IEntityTypeConfiguration<Credit>
{
    public void Configure(EntityTypeBuilder<Credit> builder)
    {
        builder.HasKey(credit => credit.Id);

        builder.Property(credit => credit.Id)
               .IsRequired();

        builder.Property(credit => credit.CreditTypeId)
               .IsRequired();

        builder.Property(credit => credit.AccountNumber)
               .IsRequired();

        builder.Property(credit => credit.CreditNumber)
               .IsRequired();

        builder.Property(credit => credit.CreditAmount)
               .IsRequired();

        builder.Property(credit => credit.PaymentPeriod)
               .IsRequired();

        builder.Property(credit => credit.InterestRate)
               .IsRequired();

        builder.Property(credit => credit.ContractDate)
               .IsRequired();

        builder.Property(credit => credit.MaturityDate)
               .IsRequired();

        builder.Property(credit => credit.MonthlyPaymentAmount)
               .IsRequired();

        builder.Property(credit => credit.NextPaymentDate)
               .IsRequired();

        builder.Property(credit => credit.RemainingDebt)
               .IsRequired();

        builder.Property(credit => credit.CurrencyId)
               .IsRequired();

        builder.Property(credit => credit.CreditStatus)
               .IsRequired();

        builder.Property(credit => credit.InterestType)
               .IsRequired();

        builder.HasOne(credit => credit.Account)
               .WithMany()
               .HasForeignKey(credit => credit.AccountNumber)
               .HasPrincipalKey(account => account.Number)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(credit => credit.CreditType)
               .IsRequired();

        builder.HasOne(credit => credit.Currency)
               .WithMany()
               .HasForeignKey(credit => credit.CurrencyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
