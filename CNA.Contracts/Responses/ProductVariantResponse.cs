namespace CNA.Contracts.Responses
{
    public class ProductVariantResponse
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = default!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public Dictionary<string, string> Attributes { get; set; } = new();
    }

}
