using CNA.Domain.Catalog;
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
                .IsRequired();

            // 1 Cart per User
            builder.HasIndex(c => c.UserId)
                .IsUnique();

            builder.HasMany(typeof(CartItem), "_items")
                .WithOne()
                .HasForeignKey("CartId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(nameof(Cart.Items))
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

