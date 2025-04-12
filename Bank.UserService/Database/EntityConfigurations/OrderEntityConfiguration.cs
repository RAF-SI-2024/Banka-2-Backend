using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);

        builder.Property(order => order.Id)
               .IsRequired();

        builder.Property(order => order.ActuaryId)
               .IsRequired();

        builder.Property(order => order.OrderType)
               .IsRequired();

        builder.Property(order => order.Quantity)
               .IsRequired();

        builder.Property(order => order.ContractCount)
               .IsRequired();

        builder.Property(order => order.PricePerUnit)
               .IsRequired();

        builder.Property(order => order.Direction)
               .IsRequired();

        builder.Property(order => order.Status)
               .IsRequired();

        builder.Property(order => order.SupervisorId)
               .IsRequired();

        builder.Property(order => order.Done)
               .IsRequired();

        builder.Property(order => order.RemainingPortions)
               .IsRequired();

        builder.Property(order => order.AfterHours)
               .IsRequired();

        builder.Property(order => order.CreatedAt)
               .IsRequired();

        builder.Property(order => order.ModifiedAt)
               .IsRequired();

        builder.HasOne(order => order.Actuary)
               .WithMany()
               .HasForeignKey(order => order.ActuaryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(order => order.Supervisor)
               .WithMany()
               .HasForeignKey(order => order.SupervisorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
