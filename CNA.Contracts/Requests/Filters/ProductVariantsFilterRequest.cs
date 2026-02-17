namespace CNA.Contracts.Requests.Filters
{
    public record ProductVariantsFilterRequest
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Brand { get; set; }
        public string? SearchText { get; set; }
        public int PageSize { get; set; } = 20;
    }
}
