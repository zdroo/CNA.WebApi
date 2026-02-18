namespace CNA.Contracts.Responses
{
    public record OrderItemResponse(Guid ProductVariantId, int Quantity, decimal Price, decimal Total);
}
