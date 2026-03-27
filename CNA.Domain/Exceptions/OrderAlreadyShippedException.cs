namespace CNA.Domain.Exceptions
{
    public class OrderAlreadyShippedException : DomainException
    {
        public OrderAlreadyShippedException() : base("Cannot cancel an order that has already been shipped.") { }
    }
}
