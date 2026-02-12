namespace CNA.Application.Catalog.Queries.Filters
{
    public class ProductVariantsFilter
    {
        public Guid ProductId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Brand { get; set; }
        public string? SearchText { get; set; }
        public int PageSize { get; set; } = 20;
    }
}
