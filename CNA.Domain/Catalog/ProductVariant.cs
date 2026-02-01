using CNA.Domain.Common;

namespace CNA.Domain.Catalog
{
    public class ProductVariant : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; } = default!;
        public string Sku { get; private set; } = default!;
        public decimal Price { get; private set; }
        public Stock Stock { get; private set; } = default!;

        private readonly List<VariantAttribute> _attributes = new();
        public IReadOnlyCollection<VariantAttribute> Attributes => _attributes;

        public bool IsActive { get; private set; } = true;

        protected ProductVariant() { }

        public ProductVariant(Guid productId, string sku, decimal price)
        {
            ProductId = productId;
            Sku = sku;
            Price = price;
        }

        public void AddAttribute(string name, string value)
        {
            _attributes.Add(new VariantAttribute(name, value));
        }
    }
}
