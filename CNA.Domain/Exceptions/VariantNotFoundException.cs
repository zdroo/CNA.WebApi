namespace CNA.Domain.Exceptions
{
    public class VariantNotFoundException : DomainException
    {
        public VariantNotFoundException(Guid variantId)
            : base($"Variant not found '{variantId}'") { }
    }
}
