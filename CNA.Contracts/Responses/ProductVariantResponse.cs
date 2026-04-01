namespace CNA.Contracts.Responses
{
    public record ProductVariantResponse(
        string VariantSlug,
        Guid ProductVariantId,
        Guid ProductId,
        Guid CategoryId,
        string CategoryName,
        string ProductName,
        string Sku, 
        string Description, 
        string Brand, 
        decimal Price, 
        int StockQuantity, 
        Dictionary<string, string> Attributes
    );
}
