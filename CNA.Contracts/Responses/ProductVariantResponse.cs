namespace CNA.Contracts.Responses
{
    public record ProductVariantResponse(
        Guid ProductVariantId,
        string Sku, 
        string Description, 
        string Brand, 
        decimal Price, 
        int StockQuantity, 
        Dictionary<string, string> Attributes
    );
}
