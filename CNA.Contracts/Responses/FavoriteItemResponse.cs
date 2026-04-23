namespace CNA.Contracts.Responses
{
    public record FavoriteItemResponse
    {
        public Guid FavoriteItemId { get; init; }
        public Guid ProductVariantId { get; init; }
        public string VariantSlug { get; init; } = string.Empty;
        public string ProductSlug { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string? Brand { get; init; }
        public decimal Price { get; init; }
        public string? PrimaryImageUrl { get; init; }
        public int StockQuantity { get; init; }
    }
}
