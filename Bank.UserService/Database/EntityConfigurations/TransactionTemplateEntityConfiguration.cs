using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class TransactionTemplateEntityConfiguration : IEntityTypeConfiguration<TransactionTemplate>
{
    public void Configure(EntityTypeBuilder<TransactionTemplate> builder)
    {
        builder.HasKey(transactionTemplate => transactionTemplate.Id);

        builder.Property(transactionTemplate => transactionTemplate.Id)
               .IsRequired();

        builder.Property(transactionTemplate => transactionTemplate.Name)
               .HasMaxLength(32)
               .IsRequired();

        builder.Property(transactionTemplate => transactionTemplate.AccountNumber)
               .IsRequired()
               .HasMaxLength(18);

        builder.HasOne(transactionTemplate => transactionTemplate.Client)
               .WithMany(user => user.TransactionTemplates)
               .HasForeignKey(transactionTemplate => transactionTemplate.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(transactionTemplate => transactionTemplate.Deleted)
               .IsRequired();
    }
}
