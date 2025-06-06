using Bank.ExchangeService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.ExchangeService.Database.EntityConfigurations;

public class QuoteEntityConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.HasKey(quote => quote.Id);

        builder.HasIndex(quote => quote.CreatedAt);

        builder.Property(quote => quote.Id)
               .IsRequired();

        builder.Property(quote => quote.AskPrice)
               .IsRequired()
               .HasPrecision(28, 12);

        builder.Property(quote => quote.BidPrice)
               .IsRequired()
               .HasPrecision(28, 12);

        builder.Property(quote => quote.HighPrice)
               .IsRequired()
               .HasPrecision(28, 12);

        builder.Property(quote => quote.LowPrice)
               .IsRequired()
               .HasPrecision(28, 12);

        builder.Property(quote => quote.ImpliedVolatility)
               .IsRequired()
               .HasPrecision(28, 12);

        builder.Property(quote => quote.ClosePrice)
               .IsRequired()
               .HasPrecision(28, 12);

        builder.Property(quote => quote.OpeningPrice)
               .IsRequired()
               .HasPrecision(28, 12);

        builder.Property(quote => quote.Volume)
               .IsRequired();

        builder.Property(quote => quote.CreatedAt)
               .IsRequired();

        builder.Property(quote => quote.ModifiedAt)
               .IsRequired();

        builder.Property(quote => quote.SecurityId)
               .IsRequired();

        builder.HasOne(quote => quote.Security)
               .WithMany()
               .HasForeignKey(quote => quote.SecurityId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(quote => new { quote.SecurityId, quote.CreatedAt })
               .IncludeProperties(quote => new { quote.OpeningPrice, quote.HighPrice, quote.LowPrice, quote.ClosePrice, quote.Volume });
    }
}
