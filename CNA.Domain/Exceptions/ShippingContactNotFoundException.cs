namespace CNA.Domain.Exceptions
{
    public class ShippingContactNotFoundException : DomainException
    {
        public ShippingContactNotFoundException(Guid shippingContactId, Guid userId)
            : base($"Shipping Contact with ID '{shippingContactId}' was NOT found for user '{userId}'.") { }
    }
}
