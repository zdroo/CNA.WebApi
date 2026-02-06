using CNA.Domain.Common;
using CNA.Domain.Exceptions;

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

        internal Stock(int quantity)
        {
            if (quantity < 0)
                throw new InvalidOperationException("Stock cannot be negative");

            Quantity = quantity;
        }

        internal void Increase(int amount)
        {
            Quantity += amount;
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
