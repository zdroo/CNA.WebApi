namespace CNA.Domain.Exceptions
{
    public class VariantNotExistingException : DomainException
    {
        public VariantNotExistingException(Guid VariantId)
            : base($"Variant no longer exists '{VariantId}'") { }
    }
}
