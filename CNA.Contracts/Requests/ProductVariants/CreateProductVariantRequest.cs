namespace CNA.Contracts.Requests.ProductVariants
{
    public record CreateProductVariantRequest(string Sku, string Name, decimal Price, List<VariantAttribute> VariantAttributes);
}



