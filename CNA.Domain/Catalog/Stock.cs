using CNA.Domain.Common;

namespace CNA.Domain.Catalog
{
    public class Stock : BaseEntity
    {
        public Guid ProductVariantId { get; private set; }
        public ProductVariant ProductVariant { get; private set; } = default!;

        public int Quantity { get; private set; }

        protected Stock() { }

        public Stock(Guid productVariantId, int quantity)
        {
            ProductVariantId = productVariantId;
            Quantity = quantity;
        }

        public void Decrease(int amount)
        {
            if (Quantity < amount)
                throw new InvalidOperationException("Insufficient stock");

            Quantity -= amount;
            MarkUpdated();
        }
    }

}
