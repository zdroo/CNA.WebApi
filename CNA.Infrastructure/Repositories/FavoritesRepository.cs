using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories
{
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly CNADbContext _context;

        public FavoritesRepository(CNADbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FavoriteItem favorite)
        {
            await _context.FavoriteItems.AddAsync(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FavoriteItem>> GetAllAsync(Guid userId)
        {
            return await _context.FavoriteItems.Where(f => f.UserId == userId).ToListAsync();
        }

        public async Task<List<FavoriteItem>> GetAllAsync(string sessionId)
        {
            return await _context.FavoriteItems.Where(f => f.SessionId == sessionId).ToListAsync();
        }
    }
}

