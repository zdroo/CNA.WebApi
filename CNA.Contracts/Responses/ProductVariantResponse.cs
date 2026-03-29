namespace CNA.Contracts.Responses
{
    public record ProductVariantResponse(
        Guid ProductVariantId,
        Guid ProductId,
        Guid CategoryId,
        string Sku, 
        string Description, 
        string Brand, 
        decimal Price, 
        int StockQuantity, 
        Dictionary<string, string> Attributes
    );
}
