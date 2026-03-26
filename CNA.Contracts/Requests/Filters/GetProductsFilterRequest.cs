namespace CNA.Contracts.Requests.Filters
{
    public record GetProductsFilterRequest
    {
        public Guid? CategoryId { get; set; }
        public string? SearchText { get; set; }
        public int PageSize { get; set; } = 10;
        public bool IsFeatured { get; set; }
    }
}
