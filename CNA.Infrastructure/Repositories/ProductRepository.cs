using CNA.Application.Catalog.Queries.ProductVariant;
using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
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

        public async Task<List<ProductVariant>> GetByProductId(Guid productId)
        {
            return await _context.ProductVariants
                .Include(x => x.Attributes)
                .AsNoTracking()
                .Where(p => p.ProductId == productId).ToListAsync();
        }

        public async Task<ProductVariant?> GetByProductVariantId(Guid productVariantId)
        {
            return await _context.ProductVariants
                .Include(x => x.Attributes)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == productVariantId);
        }

        public async Task<List<ProductVariant>> GetFiltered(GetProductVariantsQuery filter)
        {
            var query = _context.ProductVariants
                .AsNoTracking()
                .Where(p => p.ProductId == filter.ProductId);

            var customFilter = filter.Filter;

            if (customFilter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= customFilter.MinPrice.Value);

            if (customFilter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= customFilter.MaxPrice.Value);

            if (!string.IsNullOrWhiteSpace(customFilter.Brand))
                query = query.Where(p => p.Brand == customFilter.Brand);

            if (!string.IsNullOrWhiteSpace(customFilter.SearchText))
                query = query.Where(p =>
                    EF.Functions.Like(p.Name, $"%{customFilter.SearchText}%") ||
                    EF.Functions.Like(p.Description, $"%{customFilter.SearchText}%"));

            return await query.ToListAsync();
        }
    }
}
