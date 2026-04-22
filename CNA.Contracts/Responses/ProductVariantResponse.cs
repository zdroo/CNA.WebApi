namespace CNA.Contracts.Responses
{
    public record ProductVariantResponse
    {
        public string VariantSlug { get; init; } = string.Empty;
        public string ProductSlug { get; init; } = string.Empty;
        public Guid VariantId { get; init; }
        public Guid ProductId { get; init; }
        public Guid CategoryId { get; init; }
        public string CategoryName { get; init; } = string.Empty;
        public string ProductName { get; init; } = string.Empty;
        public string Sku { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Brand { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public string? PrimaryImageUrl { get; init; }
        public List<string> ImageUrls { get; init; } = new();
        public Dictionary<string, string> Attributes { get; init; } = new();
        public decimal AverageRating { get; init; }
        public List<ReviewResponse> Reviews { get; init; } = new();

        public ProductVariantResponse()
        {
        }

        public ProductVariantResponse(
            string variantSlug,
            Guid productVariantId,
            Guid productId,
            Guid categoryId,
            string categoryName,
            string productName,
            string sku,
            string description,
            string brand,
            decimal price,
            int stockQuantity,
            Dictionary<string, string> attributes)
        {
            VariantSlug = variantSlug;
            VariantId = productVariantId;
            ProductId = productId;
            CategoryId = categoryId;
            CategoryName = categoryName;
            ProductName = productName;
            Sku = sku;
            Description = description;
            Brand = brand;
            Price = price;
            StockQuantity = stockQuantity;
            Attributes = attributes;
        }
    }
}
