using CNA.Contracts.Responses;
using CNA.Domain.Catalog.Entities;
using CNA.Domain.Filters;

namespace CNA.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Product>> ListAllAsync();
        Task AddAsync(Product product);
        Task DeleteAsync(Product product);
        Task<IReadOnlyList<Product>> ListByCategoryAsync(string categorySlug);


        Task<List<ProductVariant>> GetByProductId(Guid productId);
        Task<List<ProductVariant>> GetFiltered(ProductVariantsFilter filter);
        Task<ProductVariant?> GetVariantBySlug(string variantSlug);
        Task<ProductVariant?> GetVariantById(Guid variantId);
        Task<List<ProductVariant>> GetByProductVariantIds(IEnumerable<Guid> productVariantIds);
        Task<List<VariantAttribute>> GetVariantFiltersAsync(AttributesFilter filter);
    }
}
