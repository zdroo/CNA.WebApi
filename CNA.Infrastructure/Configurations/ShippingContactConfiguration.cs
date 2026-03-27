using CNA.Domain.Catalog.Entities.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class ShippingContactConfiguration : IEntityTypeConfiguration<ShippingContact>
    {
        public void Configure(EntityTypeBuilder<ShippingContact> builder)
        {
            builder.ToTable("ShippingContacts");

            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.FullName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(sc => sc.PhoneNumber)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(sc => sc.IsDefault)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(sc => sc.User)
                .WithMany(u => u.ShippingContacts)
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sc => sc.Address)
                .WithMany()
                .HasForeignKey(sc => sc.AddressId)
                .OnDelete(DeleteBehavior.Restrict);  // prevent cascade conflict
        }
    }
}
