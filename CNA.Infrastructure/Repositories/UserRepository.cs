using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CNADbContext _context;

        public UserRepository(CNADbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.RefreshTokens)
                .Include(u => u.Cart)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.RefreshTokens)
                .Include(u => u.Cart)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
