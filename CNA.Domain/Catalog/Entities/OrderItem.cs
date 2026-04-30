using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid ProductVariantId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public string VariantSlug { get; private set; } = string.Empty;
        public string ProductSlug { get; private set; } = string.Empty;

        protected OrderItem() { }

        public OrderItem(Guid productVariantId, int quantity, decimal price, string productName, string variantSlug, string productSlug)
        {
            ProductVariantId = productVariantId;
            Quantity = quantity;
            Price = price;
            ProductName = productName;
            VariantSlug = variantSlug;
            ProductSlug = productSlug;
        }

        public decimal Total => Price * Quantity;
    }
}
