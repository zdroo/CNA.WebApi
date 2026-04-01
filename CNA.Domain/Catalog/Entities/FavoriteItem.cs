using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities
{
    public class FavoriteItem : BaseEntity
    {
        public Guid ProductVariantId { get; private set; }
        public Guid? UserId { get; private set; } // null pentru anonimi
        public string? SessionId { get; private set; } // folosit pentru anonimi

        public ProductVariant Product { get; private set; } = default!;
        public User? User { get; private set; }

        private FavoriteItem() { }

        public FavoriteItem(Guid productVariantId, Guid? userId = null, string? sessionId = null)
        {
            ProductVariantId = productVariantId;
            UserId = userId;
            SessionId = sessionId;
        }
    }
}
