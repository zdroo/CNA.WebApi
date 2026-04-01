using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class VariantImageConfiguration : IEntityTypeConfiguration<VariantImage>
    {
        public void Configure(EntityTypeBuilder<VariantImage> builder)
        {
            builder.ToTable("VariantImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Url)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.SortOrder)
                .IsRequired();

            builder.Property(x => x.ProductVariantId)
                .IsRequired();

            builder.HasIndex(x => new { x.ProductVariantId, x.SortOrder })
                .IsUnique();

            builder.HasOne(x => x.ProductVariant)
                .WithMany(v => v.Images)
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}