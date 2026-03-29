namespace CNA.Domain.Exceptions
{
    public class OrderNotFoundException : DomainException
    {
        public OrderNotFoundException(Guid orderId)
            : base($"Order not found '{orderId}'") { }
    }
}
