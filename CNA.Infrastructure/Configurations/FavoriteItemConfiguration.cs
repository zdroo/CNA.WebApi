using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations;

public class FavoriteItemConfiguration : IEntityTypeConfiguration<FavoriteItem>
{
    public void Configure(EntityTypeBuilder<FavoriteItem> builder)
    {
        builder.ToTable("FavoriteItems");

        builder.HasKey(f => f.Id);

        // Relație cu ProductVariant
        builder.HasOne(f => f.Product)
            .WithMany() // nu trebuie neaparat navigație inversă
            .HasForeignKey(f => f.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relație opțională cu User
        builder.HasOne(f => f.User)
            .WithMany() // navigație inversă optională
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Proprietate SessionId
        builder.Property(f => f.SessionId)
            .HasMaxLength(100)
            .IsRequired(false);

        // Index pentru căutări rapide
        builder.HasIndex(f => f.UserId);
        builder.HasIndex(f => f.SessionId);
        builder.HasIndex(f => f.ProductVariantId);
    }
}