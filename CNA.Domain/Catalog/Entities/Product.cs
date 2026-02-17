using CNA.Domain.Common;
using CNA.Domain.Exceptions;

namespace CNA.Domain.Catalog.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
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
            Guid categoryId)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
        }

        public void AddVariant(ProductVariant variant)
        {
            _variants.Add(variant);
        }

        public ProductVariant AddVariant(string sku, decimal price, string description, string brand, int quantity, IEnumerable<(string Name, string Value)> attributes) 
        { 
            var variant = new ProductVariant(this.Id, sku, price, description, brand); 

            foreach (var attr in attributes) 
            { 
                variant.AddAttribute(attr.Name, attr.Value); 
            
            }

            variant.Stock.Increase(quantity);

            _variants.Add(variant); 
            return variant; 
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

        public void UpdateVariant(
            Guid variantId,
            string? name,
            string? sku,
            decimal? price,
            int? quantity,
            IEnumerable<(string Name, string Value)>? attributes,
            bool? isActive)
        {
            var variant = _variants.FirstOrDefault(v => v.Id == variantId)
                ?? throw new Exception("Variant not found");

            variant.UpdateDetails(name, sku, price, isActive);

            variant.ClearAttributes();

            if (attributes != null)
            {
                foreach (var attr in attributes)
                {
                    variant.AddAttribute(attr.Name, attr.Value);
                }
            }

            if (quantity.HasValue)
            {
                variant.Stock.Increase(quantity.Value);
            }
        }

        public void UpdateProduct(string? name, string? description, string? brand, Guid? categoryId, bool? isActive, bool? isShippable, bool? isDigital, bool? isReturnable)
        {
            if (name != null)
                Name = name;

            if (description != null)
                Description = description;

            if (categoryId != null)
                CategoryId = categoryId.Value;

            if (isActive.HasValue)
                IsActive = isActive.Value;

            if (isShippable.HasValue)
                IsShippable = isShippable.Value;

            if (isDigital.HasValue)
                IsDigital = isDigital.Value;

            if (isReturnable.HasValue)
                IsReturnable = isReturnable.Value;
        }
    }
}
