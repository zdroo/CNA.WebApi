using CNA.Application.Catalog.Queries;
using CNA.Domain.Catalog;

namespace CNA.Application.Interfaces
{
    public interface IProductVariantRepository
    {
        Task<List<ProductVariant>> GetFiltered(GetProductVariantsQuery filter);
    }
}
