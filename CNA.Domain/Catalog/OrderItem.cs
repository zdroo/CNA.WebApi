using CNA.Domain.Common;

namespace CNA.Domain.Catalog
{
    public class OrderItem : BaseEntity
    {
        public Guid ProductVariantId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        protected OrderItem() { }

        public OrderItem(Guid productVariantId, int quantity, decimal price)
        {
            ProductVariantId = productVariantId;
            Quantity = quantity;
            Price = price;
        }

        public decimal Total => Price * Quantity;
    }
}
