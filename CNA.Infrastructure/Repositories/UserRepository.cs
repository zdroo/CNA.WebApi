using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;

namespace CNA.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CNADbContext _context;

        public UserRepository(CNADbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException(); 
        }
        public async Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
