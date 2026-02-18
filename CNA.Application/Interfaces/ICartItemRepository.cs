using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem?> GetByIdAsync(Guid cartItemId);
        Task RemoveCartItemAsync(CartItem cartItem);
    }
}
