using CNA.Domain.Catalog.Enums;
using CNA.Domain.Models;

namespace CNA.Domain.Filters
{
    public class ProductVariantsFilter
    {
        public Guid? ProductId { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Brand { get; set; }
        public PriceRange? PriceRange { get; set; }
        public string? SearchText { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public bool OnlyActive { get; set; } = true;
        public bool OnlyInStock { get; set; } = true;
        public bool Featured { get; set; }
        public ProductSortBy? SortBy { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }
}
