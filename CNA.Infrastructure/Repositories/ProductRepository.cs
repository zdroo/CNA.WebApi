using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using CNA.Domain.Catalog.Enums;
using CNA.Domain.Filters;
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
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Reviews)
                .Include(p => p.Category)
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

        public async Task<IReadOnlyList<Product>> ListByCategoryAsync(string categorySlug)
        {
            var products = _context.Products
                .Include(p => p.Category).ToList();

            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.Slug == categorySlug)
                .ToListAsync();
        }

        public async Task<List<Product>> GetFiltered(ProductsFilter filter)
        {
            var query = _context.Products
                .Include(v => v.Variants)
                .AsQueryable();

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                query = query.Where(p =>
                    EF.Functions.Like(p.Name, $"%{filter.SearchText}%") ||
                    EF.Functions.Like(p.Description, $"%{filter.SearchText}%")
                    );
            }

            if (filter.IsFeatured)
            {
                // TODO
            }

            //if (filter.PageSize.HasValue)
            //{
            //    // TO DO
            //}

            return await query.ToListAsync();
        }

        public async Task<List<ProductVariant>> GetByProductId(Guid productId)
        {
            return await _context.ProductVariants
                .Include(x => x.Stock)
                .Include(x => x.Reviews)
                .Include(x => x.Attributes)
                .Include(v => v.Images.OrderBy(i => i.SortOrder))
                .AsNoTracking()
                .Where(p => p.ProductId == productId).ToListAsync();
        }

        public async Task<ProductVariant?> GetVariantBySlug(string variantSlug)
        {
            return await _context.ProductVariants
                .Include(x => x.Attributes)
                .Include(v => v.Product)
                .Include(v => v.Stock)
                .Include(v => v.Reviews)
                .Include(v => v.Images.OrderBy(i => i.SortOrder))
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Slug == variantSlug);
        }

        public async Task<ProductVariant?> GetVariantById(Guid variantId)
        {
            return await _context.ProductVariants
                .Include(v => v.Attributes)
                .Include(v => v.Product)
                .Include(v => v.Stock)
                .Include(v => v.Reviews)
                .Include(v => v.Images.OrderBy(i => i.SortOrder))
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == variantId);
        }

        public async Task<List<ProductVariant>> GetByProductVariantIds(IEnumerable<Guid> productVariantIds)
        {
            return await _context.ProductVariants
                .Include(x => x.Attributes)
                .Include(v => v.Images.OrderBy(i => i.SortOrder))
                .AsNoTracking()
                .Where(_ => productVariantIds.Contains(_.Id)).ToListAsync();
        }

        public async Task<List<ProductVariant>> GetFiltered(ProductVariantsFilter filter)
        {
            var query = _context.ProductVariants
                .Include(v => v.Product)
                    .ThenInclude(p => p.Category)
                .Include(v => v.Attributes)
                .Include(v => v.Stock)
                .Include(v => v.Images.OrderBy(i => i.SortOrder))
                .Include(v => v.Reviews)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.ProductSlug))
            {
                query = query.Where(p => p.Product.Slug == filter.ProductSlug);
            }

            if (filter.ProductId.HasValue)
            {
                query = query.Where(p => p.ProductId == filter.ProductId);
            }

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.Product.CategoryId == filter.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Brand))
                query = query.Where(p => p.Brand == filter.Brand);

            if (filter.PriceRange is not null)
            {
                if (filter.PriceRange.MinPrice.HasValue)
                    query = query.Where(p => p.Price >= filter.PriceRange.MinPrice.Value);

                if (filter.PriceRange.MaxPrice.HasValue)
                    query = query.Where(p => p.Price <= filter.PriceRange.MaxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
                query = query.Where(p =>
                    EF.Functions.Like(p.Name, $"%{filter.SearchText}%") ||
                    EF.Functions.Like(p.Description, $"%{filter.SearchText}%"));

            if (filter.Attributes != null)
            {
                foreach (var attr in filter.Attributes)
                {
                    var name = attr.Key;
                    var value = attr.Value;

                    query = query.Where(v =>
                        v.Attributes.Any(a =>
                            a.Name == name && a.Value == value));
                }
            }

            if (filter.OnlyActive)
            {
                query = query.Where(v => v.IsActive);
            }

            if (filter.OnlyInStock)
            {
                query = query.Where(v => v.Stock.Quantity > 0);
            }

            if (filter.Featured)
            {
                query = query.Where(v => v.IsFeatured);
            }

            query = filter.SortBy switch
            {
                ProductSortBy.PriceAsc => query.OrderBy(v => v.Price),
                ProductSortBy.PriceDesc => query.OrderByDescending(v => v.Price),
                ProductSortBy.Newest => query.OrderByDescending(v => v.CreatedAt),
                _ => query
            };

            //if (filter.PageSize.HasValue)
            //{
            //    // TO DO
            //}

            return await query.ToListAsync();
        }

        public async Task<List<VariantAttribute>> GetVariantFiltersAsync(AttributesFilter filter)
        {
            var query = _context.ProductVariants
                .Include(v => v.Attributes)
                .AsQueryable();

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(v => v.Product.CategoryId == filter.CategoryId);
            }

            if (filter.ProductId.HasValue)
            {
                query = query.Where(v => v.ProductId == filter.ProductId);
            }

            var attributes = await query
                .SelectMany(v => v.Attributes)
                .ToListAsync();

            return attributes;
        }
    }
}
