using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Services
{
    public interface ICartService
    {
        Task<Cart?> GetCartAsync(Guid? userId, Guid? sessionId, CancellationToken ct = default);
        Task<Cart> GetOrCreateCartAsync(Guid? userId, Guid? sessionId, CancellationToken ct = default);
    }
}
