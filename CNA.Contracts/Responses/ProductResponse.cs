namespace CNA.Contracts.Responses
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public bool IsActive { get; set; }

        public List<ProductVariantResponse> Variants { get; set; } = new();
    }
}
