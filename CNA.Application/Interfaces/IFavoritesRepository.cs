using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface IFavoritesRepository
    {
        Task AddAsync(FavoriteItem favorite);
        Task<FavoriteItem?> GetByIdAsync(Guid favoriteItemId);
        Task RemoveAsync(FavoriteItem favorite);
        Task<List<FavoriteItem>> GetAllAsync(Guid userId);
        Task<List<FavoriteItem>> GetAllAsync(string sessionId);
    }
}
