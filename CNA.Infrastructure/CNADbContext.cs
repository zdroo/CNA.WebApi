using CNA.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure
{
    public class CNADbContext : DbContext
    {
        public CNADbContext(DbContextOptions<CNADbContext> options)
            : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<VariantAttribute> VariantAttributes => Set<VariantAttribute>();
        public DbSet<Stock> Stocks => Set<Stock>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product -> Variant (1:N)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Variants)
                .WithOne(v => v.Product)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Variant -> Attributes (1:N)
            modelBuilder.Entity<ProductVariant>()
                .HasMany(v => v.Attributes)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Variant -> Stock (1:1)
            modelBuilder.Entity<ProductVariant>()
                .HasOne(v => v.Stock)
                .WithOne()
                .HasForeignKey<Stock>(s => s.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
