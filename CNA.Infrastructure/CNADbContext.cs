using CNA.Domain.Catalog;
using CNA.Infrastructure.Configurations;
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

            modelBuilder.ApplyConfiguration(new ProductVariantConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new VariantAttributeConfiguration());
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Product>()
        //        .HasMany(p => p.Variants)
        //        .WithOne(v => v.Product)
        //        .HasForeignKey(v => v.ProductId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<ProductVariant>()
        //        .HasMany(v => v.Attributes)
        //        .WithOne()
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<ProductVariant>()
        //        .HasMany(v => v.Reviews)
        //        .WithOne()
        //        .HasForeignKey(r => r.ProductVariantId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<ProductVariant>()
        //        .HasOne(v => v.Stock)
        //        .WithOne()
        //        .HasForeignKey<Stock>(s => s.ProductVariantId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<Product>()
        //        .HasIndex(p => p.CategoryId);

        //    modelBuilder.Entity<ProductVariant>()
        //        .HasIndex(v => v.Sku)
        //        .IsUnique();

        //    modelBuilder.Entity<Review>()
        //        .HasIndex(r => r.ProductVariantId);
        //}
    }
}
