using CNA.Domain.Catalog.Enums;
using CNA.Domain.Common;

namespace CNA.Domain.Catalog
{
    public class Review : BaseEntity
    {
        public Guid ProductVariantId { get; private set; }
        public ProductVariant ProductVariant { get; private set; }
        public RatingScore Rating { get; private set; }
        public string Comment { get; private set; } = default!;
        public Guid UserId { get; private set; }

        protected Review() { }

        public Review(
            Guid productVariantId,
            RatingScore rating,
            string comment,
            Guid userId)
        {
            ProductVariantId = productVariantId;
            Rating = rating;
            Comment = comment;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
