using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(100);

            builder.Property(p => p.IsShippable)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.IsDigital)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.IsReturnable)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.Slug)
                .HasMaxLength(100);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.CategoryId);

            builder.HasMany<ProductVariant>(p => p.Variants)
                .WithOne(v => v.Product)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(nameof(Product.Variants))
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
