using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CNADbContext _context;

        public CartRepository(CNADbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(_ => _.UserId == userId);
        }
        public async Task AddAsync(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public Task<CartItem?> GetByIdAsync(Guid cartItemId)
        {
            return _context.CartItems.FirstOrDefaultAsync(_ => _.Id == cartItemId);
        }

        public async Task RemoveCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
