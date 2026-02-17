using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<User?> GetByRefreshTokenAsync(string refreshToken);
    }
}
