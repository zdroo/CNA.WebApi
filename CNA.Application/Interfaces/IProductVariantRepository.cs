using CNA.Application.Catalog.Queries.ProductVariant;
using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface IProductVariantRepository
    {
        Task<List<ProductVariant>> GetByProductId(Guid productId);
        Task<List<ProductVariant>> GetFiltered(GetProductVariantsQuery filter);
        Task<ProductVariant?> GetByProductVariantId(Guid productVariantId);
        Task<List<ProductVariant>> GetByProductVariantIds(IEnumerable<Guid> productVariantIds);
    }
}
