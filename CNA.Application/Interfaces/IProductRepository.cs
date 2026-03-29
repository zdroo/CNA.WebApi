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
        Task<IReadOnlyList<Product>> ListByCategoryAsync(Guid categoryId);


        Task<List<ProductVariant>> GetByProductId(Guid productId);
        Task<List<ProductVariant>> GetFiltered(ProductVariantsFilter filter);
        Task<ProductVariant?> GetByProductVariantId(Guid productVariantId);
        Task<List<ProductVariant>> GetByProductVariantIds(IEnumerable<Guid> productVariantIds);
        Task<List<VariantAttribute>> GetVariantFiltersAsync(AttributesFilter filter);
    }
}
