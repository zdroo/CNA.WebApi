using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly CNADbContext _context;

        public CartItemRepository(CNADbContext context)
        {
            _context = context;
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
