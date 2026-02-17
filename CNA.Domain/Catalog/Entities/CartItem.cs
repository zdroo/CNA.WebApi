using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities
{
    public class CartItem : BaseEntity
    {
        public Guid ProductVariantId { get; private set; }
        public Guid CartId { get; private set; }
        public Cart Cart { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        protected CartItem() { }

        public CartItem(Guid productVariantId, int quantity, decimal price)
        {
            ProductVariantId = productVariantId;
            Quantity = quantity;
            Price = price;
        }

        public void Increase(int quantity)
        {
            Quantity += quantity;
        }

        public decimal Total => Price * Quantity;
    }
}
