using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class LoanTypeEntityConfiguration : IEntityTypeConfiguration<LoanType>
{
    public void Configure(EntityTypeBuilder<LoanType> builder)
    {
        builder.HasKey(loanType => loanType.Id);

        builder.Property(loanType => loanType.Id)
               .IsRequired();

        builder.Property(loanType => loanType.Name)
               .HasMaxLength(128)
               .IsRequired();

        builder.Property(loanType => loanType.Margin)
               .IsRequired()
               .HasPrecision(18, 6);

        builder.Property(loanType => loanType.CreatedAt)
               .IsRequired();

        builder.Property(loanType => loanType.ModifiedAt)
               .IsRequired();
    }
}
