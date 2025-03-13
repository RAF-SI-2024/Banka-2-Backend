using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class TransactionCodeEntityConfiguration : IEntityTypeConfiguration<TransactionCode>
{
    public void Configure(EntityTypeBuilder<TransactionCode> builder)
    {
        builder.HasKey(transactionCode => transactionCode.Id);

        builder.Property(transactionCode => transactionCode.Id)
               .IsRequired();

        builder.Property(transactionCode => transactionCode.Code)
               .HasMaxLength(3)
               .IsFixedLength()
               .IsRequired();

        builder.Property(transactionCode => transactionCode.Name)
               .HasMaxLength(64)
               .IsRequired();
    }
}
