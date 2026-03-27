using CNA.Domain.Catalog.Enums;
using CNA.Domain.Catalog.ValueObjects;
using CNA.Domain.Common;
using CNA.Domain.Exceptions;

namespace CNA.Domain.Catalog.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; private set; }
        public Guid? ShippingContactId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public ShippingAddressSnapshot ShippingAddress { get; private set; }
        public bool IsPaid { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items;

        private readonly List<OrderItem> _items = new();

        protected Order() { }

        public Order(Guid userId, Guid? shippingContactId, ShippingAddressSnapshot shippingAddress, IEnumerable<OrderItem> items)
        {
            UserId = userId;
            ShippingContactId = shippingContactId;
            ShippingAddress = shippingAddress;
            Status = OrderStatus.Pending;

            _items.AddRange(items);
            TotalAmount = _items.Sum(i => i.Total);
        }

        public void MarkAsPaid()
        {
            IsPaid = true;
        }

        public void Cancel()
        {
            if (Status >= OrderStatus.Shipped)
                throw new OrderAlreadyShippedException();

            Status = OrderStatus.Cancelled;
        }
    }
}

//Fluxul corect:

//User apasă Checkout

//Load Cart

//Pentru fiecare item:

//verifică stock

//Creează Order

//Scade stock din ProductVariant

//Salvează Order

//Golește Cart

//Redirect la Payment
