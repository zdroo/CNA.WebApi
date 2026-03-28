using CNA.Application.Catalog.Queries.Filters.Models;

namespace CNA.Application.Catalog.Filters
{
    public record ProductsFilter(Guid? CategoryId, string? SearchText, bool IsFeatured, int PageSize = 12);
}
