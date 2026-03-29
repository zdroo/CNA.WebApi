namespace CNA.Domain.Exceptions
{
    public class CartItemNotFoundException : DomainException
    {
        public CartItemNotFoundException(Guid cartItemId)
            : base($"Variant not found '{cartItemId}'") { }
    }
}
