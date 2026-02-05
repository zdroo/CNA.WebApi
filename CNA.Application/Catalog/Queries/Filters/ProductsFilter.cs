using CNA.Application.Catalog.Queries.Filters.Models;

namespace CNA.Application.Catalog.Queries.Filters
{
    public class ProductsFilter
    {
        public Guid CategoryId { get; set; }
        public string Brand { get; set; }
        public PriceRange PriceRange { get; set; }
        public string SearchText { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
