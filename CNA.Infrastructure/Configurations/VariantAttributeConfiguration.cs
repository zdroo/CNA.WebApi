using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class VariantAttributeConfiguration : IEntityTypeConfiguration<VariantAttribute>
    {
        public void Configure(EntityTypeBuilder<VariantAttribute> builder)
        {
            builder.ToTable("VariantAttributes");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Value)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(a => a.ProductVariant)
                   .WithMany(v => v.Attributes)
                   .HasForeignKey(a => a.ProductVariantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.ProductVariantId);
        }
    }
}
