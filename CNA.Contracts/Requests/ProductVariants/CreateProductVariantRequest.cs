using CNA.Contracts.Models;

namespace CNA.Contracts.Requests.ProductVariants
{
    public record CreateProductVariantRequest(string Sku, string Name, decimal Price, List<VariantAttributeRequest> VariantAttributes, int Quantity);
}