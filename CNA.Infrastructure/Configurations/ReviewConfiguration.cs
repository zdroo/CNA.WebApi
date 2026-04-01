using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rating)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(r => r.Comment)
                .HasMaxLength(1000);

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.ProductVariantId)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.HasOne(r => r.ProductVariant)
                .WithMany(v => v.Reviews)
                .HasForeignKey(r => r.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(r => r.ProductVariantId);

            builder.HasIndex(r => new { r.ProductVariantId, r.UserId })
                .IsUnique();
        }
    }
}
