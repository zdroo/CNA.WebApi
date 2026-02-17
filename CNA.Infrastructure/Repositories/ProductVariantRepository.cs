using CNA.Application.Catalog.Queries;
using CNA.Application.Interfaces;
using CNA.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly CNADbContext _context;

        public ProductVariantRepository(CNADbContext context)
        {
            _context = context;
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
