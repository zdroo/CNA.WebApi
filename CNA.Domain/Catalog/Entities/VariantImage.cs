using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities
{
    public class VariantImage : BaseEntity
    {
        public Guid ProductVariantId { get; private set; }

        public string Url { get; private set; }

        public int SortOrder { get; private set; } //0 is primary

        public ProductVariant ProductVariant { get; private set; }

        private VariantImage() { }

        public VariantImage(string url, int sortOrder)
        {
            Url = url;
            SortOrder = sortOrder;
        }
    }
}
