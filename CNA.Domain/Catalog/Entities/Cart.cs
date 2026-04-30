using CNA.Domain.Common;
using CNA.Domain.Exceptions;

namespace CNA.Domain.Catalog.Entities
{
    public class Cart : BaseEntity
    {
        public Guid? UserId { get; private set; }
        public User? User { get; private set; }
        public Guid? SessionId { get; private set; }

        private readonly List<CartItem> _items = new();
        public IReadOnlyCollection<CartItem> Items => _items;

        protected Cart() { }

        private Cart(Guid? userId, Guid? sessionId)
        {
            UserId = userId;
            SessionId = sessionId;
        }

        public static Cart ForUser(Guid userId) => new(userId, null);
        public static Cart ForSession(Guid sessionId) => new(null, sessionId);

        public CartItem? AddItem(Guid productVariantId, decimal price, int quantity = 1)
        {
            var existing = _items.FirstOrDefault(i => i.ProductVariantId == productVariantId);
            if (existing != null)
            {
                existing.Increase(quantity);
                return null;
            }

            var item = new CartItem(Id, productVariantId, quantity, price);
            _items.Add(item);
            return item;
        }

        public void RemoveItemByCartItemId(Guid cartItemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == cartItemId)
                ?? throw new CartItemNotFoundException(cartItemId);

            _items.Remove(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public decimal GetTotal() => _items.Sum(i => i.Total);
    }
}
