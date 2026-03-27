using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.RowVersion)
                .IsRowVersion();

            builder.Property(v => v.ProductId)
                .IsRequired();

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(v => v.Description)
                .HasMaxLength(1000);

            builder.Property(v => v.Brand)
                .HasMaxLength(100);

            builder.Property(v => v.Sku)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(v => v.Sku)
                .IsUnique();

            builder.Property(v => v.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(v => v.IsFeatured)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(v => v.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(v => v.Product)
                .WithMany()
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Stock)
                .WithOne(s => s.ProductVariant)
                .HasForeignKey<Stock>(s => s.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<VariantAttribute>("_attributes")
                .WithOne()
                .HasForeignKey(a => a.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
