using CNA.Application.Catalog.Queries;
using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface IProductVariantRepository
    {
        Task<List<ProductVariant>> GetByProductId(Guid productId);
        Task<List<ProductVariant>> GetFiltered(GetProductVariantsQuery filter);
        Task<ProductVariant> GetByProductVariantId(Guid productVariantId);
    }
}
