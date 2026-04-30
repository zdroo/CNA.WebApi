namespace CNA.Contracts.Responses
{
    public record CartItemResponse(
        Guid CartItemId,
        Guid ProductVariantId,
        Guid CartId,
        int Quantity,
        decimal Price,
        decimal Total,
        string Name,
        string? Brand,
        string? PrimaryImageUrl,
        string ProductSlug,
        string VariantSlug,
        int StockQuantity
    );
}
