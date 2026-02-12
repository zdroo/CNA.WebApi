using CNA.Domain.Common;

namespace CNA.Domain.Catalog
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; private set; }

        private readonly List<CartItem> _items = new();
        public IReadOnlyCollection<CartItem> Items => _items;

        protected Cart() { }

        public Cart(Guid userId)
        {
            UserId = userId;
        }

        public void AddItem(Guid productVariantId, int quantity, decimal price)
        {
            var existing = _items.FirstOrDefault(i => i.ProductVariantId == productVariantId);

            if (existing != null)
            {
                existing.Increase(quantity);
                return;
            }

            _items.Add(new CartItem(productVariantId, quantity, price));
        }

        public void RemoveItem(Guid productVariantId)
        {
            var item = _items.FirstOrDefault(i => i.ProductVariantId == productVariantId)
                ?? throw new Exception("Item not found");

            _items.Remove(item);
        }

        public decimal GetTotal() => _items.Sum(i => i.Total);
    }
}
