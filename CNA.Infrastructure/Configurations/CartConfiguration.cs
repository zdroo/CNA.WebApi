using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserId)
                .IsRequired(false);

            builder.Property(c => c.SessionId)
                .IsRequired(false);

            builder.HasIndex(c => c.UserId)
                .IsUnique()
                .HasFilter("[UserId] IS NOT NULL");

            builder.HasIndex(c => c.SessionId)
                .IsUnique()
                .HasFilter("[SessionId] IS NOT NULL");

            builder.HasMany<CartItem>(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(nameof(Cart.Items))
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
