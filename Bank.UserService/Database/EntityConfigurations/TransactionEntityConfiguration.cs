using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(transaction => transaction.Id);

        builder.Property(transaction => transaction.Id)
               .IsRequired();

        builder.Property(transaction => transaction.FromAccountId)
               .IsRequired();

        builder.Property(transaction => transaction.ToAccountId)
               .IsRequired(false);

        builder.Property(transaction => transaction.FromAmount)
               .IsRequired();

        builder.Property(transaction => transaction.FromCurrencyId)
               .IsRequired();

        builder.Property(transaction => transaction.ToAmount)
               .IsRequired();

        builder.Property(transaction => transaction.ToCurrencyId)
               .IsRequired();

        builder.Property(transaction => transaction.ReferenceNumber)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(transaction => transaction.Purpose)
               .HasMaxLength(1024)
               .IsRequired();

        builder.Property(transaction => transaction.Status)
               .IsRequired();

        builder.Property(transaction => transaction.CreatedAt)
               .IsRequired();

        builder.Property(transaction => transaction.ModifiedAt)
               .IsRequired();

        builder.HasOne(transaction => transaction.FromAccount)
               .WithMany()
               .HasForeignKey(transaction => transaction.FromAccountId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(transaction => transaction.ToAccount)
               .WithMany()
               .HasForeignKey(transaction => transaction.ToAccountId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(transaction => transaction.Code)
               .WithMany()
               .HasForeignKey(transaction => transaction.CodeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(transaction => transaction.FromCurrency)
               .WithMany()
               .HasForeignKey(transaction => transaction.FromCurrencyId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(transaction => transaction.ToCurrency)
               .WithMany()
               .HasForeignKey(transaction => transaction.ToCurrencyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
