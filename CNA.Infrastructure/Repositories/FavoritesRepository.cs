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

        public async Task<FavoriteItem?> GetByIdAsync(Guid favoriteItemId)
        {
            return await _context.FavoriteItems.FindAsync(favoriteItemId);
        }

        public async Task RemoveAsync(FavoriteItem favorite)
        {
            _context.FavoriteItems.Remove(favorite);
        }

        public async Task<List<FavoriteItem>> GetAllAsync(Guid userId)
        {
            return await _context.FavoriteItems
                .Include(f => f.Product)
                    .ThenInclude(v => v.Product)
                .Include(f => f.Product)
                    .ThenInclude(v => v.Stock)
                .Include(f => f.Product)
                    .ThenInclude(v => v.Images.OrderBy(i => i.SortOrder))
                .Where(f => f.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<FavoriteItem>> GetAllAsync(string sessionId)
        {
            return await _context.FavoriteItems
                .Include(f => f.Product)
                    .ThenInclude(v => v.Product)
                .Include(f => f.Product)
                    .ThenInclude(v => v.Stock)
                .Include(f => f.Product)
                    .ThenInclude(v => v.Images.OrderBy(i => i.SortOrder))
                .Where(f => f.SessionId == sessionId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

