using CNA.Application.Catalog.Queries.Filters.Models;

namespace CNA.Application.Catalog.Queries.Filters
{
    public record ProductsFilter(Guid? CategoryId, string? SearchText, bool IsFeatured, int PageSize = 12);
}
