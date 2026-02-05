namespace CNA.Contracts.Requests.Products
{
    public record UpdateProductRequest(
        string Name,
        string Description,
        string Brand,
        Guid CategoryId,
        bool IsActive);
}