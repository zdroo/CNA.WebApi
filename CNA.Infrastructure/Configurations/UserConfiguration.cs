using CNA.Domain.Catalog.Entities;
using CNA.Domain.Catalog.Entities.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.Role)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Address>("_addresses")
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(nameof(User.Addresses))
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany<ShippingContact>("_shippingContacts")
                .WithOne(sc => sc.User)
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(nameof(User.ShippingContacts))
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany<RefreshToken>("_refreshTokens")
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(nameof(User.RefreshTokens))
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
