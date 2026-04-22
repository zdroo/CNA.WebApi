using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly CNADbContext _context;

        public ReviewRepository(CNADbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetByProductVariantIdAsync(Guid productVariantId)
        {
            return await _context.Reviews
                .Where(r => r.ProductVariantId == productVariantId)
                .ToListAsync();
        }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
        }
    }
}
