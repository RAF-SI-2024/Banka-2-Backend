using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.UserService.Database.EntityConfigurations;

public class CompanyEntityConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(company => company.Id);
        builder.HasAlternateKey(nameof(Company.RegistrationNumber));
        builder.HasAlternateKey(nameof(Company.TaxIdentificationNumber));

        builder.Property(company => company.Id)
               .IsRequired()
               .ValueGeneratedOnAdd();

        builder.Property(company => company.Name)
               .HasMaxLength(32)
               .IsRequired();

        builder.Property(company => company.RegistrationNumber)
               .IsRequired()
               .HasMaxLength(8)
               .IsFixedLength();

        builder.Property(company => company.TaxIdentificationNumber)
               .IsRequired()
               .HasMaxLength(9)
               .IsFixedLength();

        builder.Property(company => company.ActivityCode)
               .IsRequired()
               .HasMaxLength(5);

        builder.Property(company => company.Address)
               .IsRequired()
               .HasMaxLength(64);

        builder.HasOne(company => company.MajorityOwner)
               .WithMany()
               .HasForeignKey(company => company.MajorityOwnerId);
    }
}
