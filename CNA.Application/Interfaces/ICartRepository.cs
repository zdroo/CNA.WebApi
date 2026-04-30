using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetByUserIdAsync(Guid userId);
        Task<Cart?> GetBySessionIdAsync(Guid sessionId);
        Task AddAsync(Cart cart);
        void TrackCart(Cart cart);
        Task DeleteAsync(Cart cart);
        void TrackCartItem(CartItem item);
        Task<CartItem?> GetByIdAsync(Guid cartItemId);
        Task RemoveCartItemAsync(CartItem cartItem);
    }
}
