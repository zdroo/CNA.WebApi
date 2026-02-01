using CNA.Domain.Common;

namespace CNA.Domain.Catalog
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public string Slug { get; private set; } = default!;

        public Guid? ParentCategoryId { get; private set; }
        public Category? ParentCategory { get; private set; }

        public bool IsActive { get; private set; } = true;

        protected Category() { }

        public Category(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }
    }

}
