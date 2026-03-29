namespace CNA.Domain.Exceptions
{
    public class ProductNotFoundException : DomainException
    {
        public ProductNotFoundException(Guid productId)
            : base($"Product not found '{productId}'") { }
    }
}
