using CNA.Domain.Common;

namespace CNA.Domain.Catalog
{
    public class VariantAttribute : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public string Value { get; private set; } = default!;

        protected VariantAttribute() { }

        public VariantAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

}
