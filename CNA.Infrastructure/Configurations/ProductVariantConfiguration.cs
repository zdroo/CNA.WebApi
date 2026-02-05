using CNA.Domain.Catalog;
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

            builder.Property(v => v.Sku)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(v => v.Sku)
                .IsUnique();

            builder.Property(v => v.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasMany(v => v.Attributes)
                .WithOne()
                .HasForeignKey(a => a.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Reviews)
                .WithOne(r => r.ProductVariant)
                .HasForeignKey(r => r.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Stock)
                .WithOne(s => s.ProductVariant)
                .HasForeignKey<Stock>(s => s.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
