using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Quantity)
                .IsRequired();

            builder.HasOne(s => s.ProductVariant)
                .WithOne(v => v.Stock)
                .HasForeignKey<Stock>(s => s.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => s.ProductVariantId)
                .IsUnique();
        }
    }
}
