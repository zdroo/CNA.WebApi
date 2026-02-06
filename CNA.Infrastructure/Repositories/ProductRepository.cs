using CNA.Application.Interfaces;
using CNA.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CNADbContext _context;

        public ProductRepository(CNADbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Product>> ListAllAsync()
        {
            return await _context.Products
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Attributes)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Stock)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Attributes)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Stock)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Product>> ListByCategoryAsync(Guid categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Attributes)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Stock)
                .ToListAsync();
        }
    }
}
