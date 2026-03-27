using CNA.Domain.Catalog.Entities.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CountryCode)
                .IsRequired()
                .HasMaxLength(2);   // ISO 3166-1 alpha-2

            builder.HasIndex(c => c.CountryCode)
                .IsUnique();

            builder.Property(c => c.CurrencyCode)
                .IsRequired()
                .HasMaxLength(3);   // ISO 4217

            builder.Property(c => c.PhonePrefix)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(c => c.IsShippingAvailable)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
