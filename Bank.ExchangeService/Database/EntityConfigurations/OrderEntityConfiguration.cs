using Bank.ExchangeService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.ExchangeService.Database.EntityConfigurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);

        builder.Property(order => order.Id)
               .IsRequired();

        builder.Property(order => order.ActuaryId)
               .IsRequired();

        builder.Property(order => order.AccountId)
               .IsRequired();

        builder.Property(order => order.OrderType)
               .IsRequired();

        builder.Property(order => order.Quantity)
               .IsRequired();

        builder.Property(order => order.ContractCount)
               .IsRequired();

        builder.Property(order => order.LimitPrice)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(order => order.StopPrice)
               .HasPrecision(28, 12)
               .IsRequired();

        builder.Property(order => order.Direction)
               .IsRequired();

        builder.Property(order => order.Status)
               .IsRequired();

        builder.Property(order => order.SupervisorId)
               .IsRequired(false);

        builder.Property(order => order.CreatedAt)
               .IsRequired();

        builder.Property(order => order.ModifiedAt)
               .IsRequired();

        builder.HasOne(order => order.Security)
               .WithMany()
               .HasForeignKey(order => order.SecurityId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
