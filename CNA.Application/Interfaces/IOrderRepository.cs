using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
    }
}
