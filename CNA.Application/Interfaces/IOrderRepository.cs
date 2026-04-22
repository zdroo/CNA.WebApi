using CNA.Domain.Catalog.Entities;
using CNA.Domain.Filters;

namespace CNA.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
        Task<List<Order>> GetOrders(Guid userId);
        Task<List<Order>> GetAllOrders(OrdersFilter filter);
    }
}
