using CNA.Domain.Catalog.Entities;
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
        public DbSet<User> Users => Set<User>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<CartItem> CartItems => Set<CartItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductVariantConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new VariantAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
