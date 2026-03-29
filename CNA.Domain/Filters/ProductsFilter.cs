namespace CNA.Domain.Filters
{
    public record ProductsFilter(Guid? CategoryId, string? SearchText, bool IsFeatured, int PageSize = 12);
}
