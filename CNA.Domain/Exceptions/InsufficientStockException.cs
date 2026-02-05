namespace CNA.Domain.Exceptions
{
    public class InsufficientStockException : DomainException
    {
        public InsufficientStockException(string sku)
            : base($"Insufficient stock for SKU '{sku}'") { }
    }
}
