using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNA.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.OrderId)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.Provider)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(p => p.TransactionId)
                .HasMaxLength(256);

            builder.Property(p => p.CompletedAt);

            builder.HasOne<Order>()
                .WithOne()
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
