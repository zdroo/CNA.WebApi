namespace CNA.Contracts.Responses
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
        public string ProductSlug { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal AverageRating { get; set; }
        public decimal ReviewsCount { get; set; }
    }
}
