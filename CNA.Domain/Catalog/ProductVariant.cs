using CNA.Domain.Catalog.Enums;
using CNA.Domain.Common;

namespace CNA.Domain.Catalog
{
    public class ProductVariant : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; } = default!;
        public string Sku { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; }
        public Stock Stock { get; private set; } = default!;

        private readonly List<VariantAttribute> _attributes = new();
        public IReadOnlyCollection<VariantAttribute> Attributes => _attributes;

        private readonly List<Review> _reviews = new();
        public IReadOnlyCollection<Review> Reviews => _reviews;
        public int ReviewsCount => _reviews.Count;

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

        public void ClearAttributes()
        {
            _attributes.Clear();
        }

        public void AddReview(RatingScore rating, string comment, Guid userId)
        {
            if (_reviews.Any(r => r.UserId == userId))
                throw new InvalidOperationException("User already reviewed this variant");

            var review = new Review(Id, rating, comment, userId);
            _reviews.Add(review);
        }

        public decimal GetAverageRating()
        {
            if (_reviews.Count == 0)
                return 0;

            var average = _reviews.Sum(r => (int)r.Rating) / (decimal)_reviews.Count;
            return Math.Round(average, 2);
        }

        public void IncreaseStock(int amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be positive");

            Stock.Increase(amount);
        }

        public void DecreaseStock(int amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be positive");

            Stock.Decrease(amount);
        }
        public void UpdateDetails(string? name, string? sku, decimal? price, bool? isActive)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;

            if (!string.IsNullOrWhiteSpace(sku))
                Sku = sku;

            if (price.HasValue && price.Value < 0)
                Price = price.Value;

            if (isActive.HasValue)
                IsActive = isActive.Value;
        }
    }
}
