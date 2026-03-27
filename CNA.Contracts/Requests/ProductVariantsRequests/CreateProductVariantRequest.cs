using CNA.Contracts.Models;

namespace CNA.Contracts.Requests.ProductVariants
{
    public record CreateProductVariantRequest(string Sku, string Name, decimal Price, string Description, string Brand, int Quantity, List<VariantAttributeRequest> VariantAttributes);
}