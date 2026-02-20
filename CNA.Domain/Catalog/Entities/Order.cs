using CNA.Domain.Catalog.Enums;
using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; private set; }
        public OrderStatus Status { get; private set; }
        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items;
        public decimal TotalAmount { get; private set; }
        public string ShippingFirstName { get; private set; }
        public string ShippingLastName { get; private set; }
        public string ShippingAddressLine1 { get; private set; }
        public string ShippingCity { get; private set; }
        public string ShippingPostalCode { get; private set; }
        public string ShippingCountry { get; private set; }
        public string ShippingPhone { get; private set; }

        protected Order() { }

        public Order(Guid userId, IEnumerable<OrderItem> items)
        {
            UserId = userId;
            Status = OrderStatus.Pending;

            _items.AddRange(items);
            TotalAmount = _items.Sum(i => i.Total);
        }

        public void MarkAsPaid()
        {
            Status = OrderStatus.Paid;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Paid)
                throw new Exception("Cannot cancel paid order");

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
