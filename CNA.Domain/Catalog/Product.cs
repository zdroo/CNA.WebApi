using CNA.Domain.Common;
using CNA.Domain.Exceptions;

namespace CNA.Domain.Catalog
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string Brand { get; private set; } = default!;

        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; } = default!;

        public bool IsShippable { get; private set; } = true;
        public bool IsDigital { get; private set; }
        public bool IsReturnable { get; private set; } = true;

        public bool IsActive { get; private set; } = true;

        private readonly List<ProductVariant> _variants = new();
        public IReadOnlyCollection<ProductVariant> Variants => _variants;

        protected Product() { }

        public Product(
            string name,
            string description,
            string brand,
            Guid categoryId)
        {
            Name = name;
            Description = description;
            Brand = brand;
            CategoryId = categoryId;
        }

        public void AddVariant(ProductVariant variant)
        {
            _variants.Add(variant);
        }
        public void RemoveVariant(ProductVariant variant) 
        { 
            _variants.Remove(variant); 
        }
        public void RemoveVariant(Guid variantId)
        {
            var variant = _variants.FirstOrDefault(x => x.Id == variantId);

            if (variant != null)
            {
                _variants.Remove(variant);
            }

            throw new VariantNotExistingException(variantId);
        }
    }

}
