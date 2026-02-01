namespace CNA.Contracts.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; }

        public List<ProductVariantResponse> Variants { get; set; } = new();
    }
}
