using CNA.Application.Catalog.Queries.ProductVariant;
using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Product>> ListAllAsync();
        Task AddAsync(Product product);
        Task DeleteAsync(Product product);
        Task<IReadOnlyList<Product>> ListByCategoryAsync(Guid categoryId);

        Task<List<ProductVariant>> GetByProductId(Guid productId);
        Task<List<ProductVariant>> GetFiltered(GetProductVariantsQuery filter);
        Task<ProductVariant?> GetByProductVariantId(Guid productVariantId);
    }
}
