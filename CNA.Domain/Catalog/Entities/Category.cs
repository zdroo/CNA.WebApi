using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public string Slug { get; private set; } = default!;
        public Guid? ParentCategoryId { get; private set; }
        public Category? ParentCategory { get; private set; }
        public bool IsActive { get; private set; } = true;
        public string? ImageUrl { get; set; }

        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products;

        protected Category() { }

        public Category(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }

        public void UpdateCategory(string name, string slug)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }

            if (string.IsNullOrWhiteSpace(slug))
            {
                Slug = slug;
            }
        }
    }
}
