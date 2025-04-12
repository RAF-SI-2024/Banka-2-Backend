using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class BankEntityConfiguration : IEntityTypeConfiguration<Models.Bank>
{
    public void Configure(EntityTypeBuilder<Models.Bank> builder)
    {
        builder.HasKey(bank => bank.Id);

        builder.Property(bank => bank.Id)
               .IsRequired();

        builder.Property(bank => bank.Name)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(bank => bank.Code)
               .HasMaxLength(3)
               .IsFixedLength()
               .IsRequired();

        builder.Property(bank => bank.BaseUrl)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(bank => bank.CreatedAt)
               .IsRequired();

        builder.Property(bank => bank.ModifiedAt)
               .IsRequired();
    }
}
