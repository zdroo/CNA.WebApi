using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetByProductVariantIdAsync(Guid productVariantId);
        Task AddAsync(Review review);
    }
}
