using CNA.Domain.Catalog.Entities.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AddressLine1)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(a => a.AddressLine2)
                .HasMaxLength(256);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Region)
                .HasMaxLength(100);

            builder.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(a => a.CountryCode)
                .IsRequired()
                .HasMaxLength(2);   // ISO 3166-1 alpha-2

            builder.Property(a => a.IsDefault)
                .IsRequired()
                .HasDefaultValue(false)
                .ValueGeneratedNever();

            builder.HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
