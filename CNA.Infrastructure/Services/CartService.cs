using CNA.Application.Interfaces;
using CNA.Application.Services;
using CNA.Domain.Catalog.Entities;
using CNA.Domain.Exceptions;

namespace CNA.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;

        public CartService(IUserRepository userRepository, ICartRepository cartRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public async Task<Cart?> GetCartAsync(Guid? userId, Guid? sessionId, CancellationToken ct = default)
        {
            if (userId.HasValue)
            {
                var user = await _userRepository.GetByIdAsync(userId.Value)
                    ?? throw new UserNotFoundException(userId.Value);
                return user.Cart;
            }

            if (sessionId.HasValue)
                return await _cartRepository.GetBySessionIdAsync(sessionId.Value);

            return null;
        }

        public async Task<Cart> GetOrCreateCartAsync(Guid? userId, Guid? sessionId, CancellationToken ct = default)
        {
            if (userId.HasValue)
            {
                var user = await _userRepository.GetByIdAsync(userId.Value)
                    ?? throw new UserNotFoundException(userId.Value);
                bool isNew = user.Cart == null;
                var cart = user.GetOrCreateCart();
                if (isNew)
                    await _cartRepository.AddAsync(cart);
                return cart;
            }

            if (sessionId.HasValue)
            {
                var cart = await _cartRepository.GetBySessionIdAsync(sessionId.Value);
                if (cart == null)
                {
                    cart = Cart.ForSession(sessionId.Value);
                    await _cartRepository.AddAsync(cart);
                }
                return cart;
            }

            throw new UnauthorizedAccessException("Cart access requires authentication or a session ID.");
        }
    }
}
