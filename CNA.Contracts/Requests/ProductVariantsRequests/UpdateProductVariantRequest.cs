using CNA.Contracts.Models;

namespace CNA.Contracts.Requests.ProductVariants
{
    public record UpdateProductVariantRequest(
        string? Name,
        string? Sku,
        decimal? Price,
        bool? IsActive,
        int? Quantity,
        List<VariantAttributeRequest> VariantAttributes);
}
