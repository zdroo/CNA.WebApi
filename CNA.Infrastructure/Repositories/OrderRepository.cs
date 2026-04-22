using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using CNA.Domain.Filters;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CNADbContext _context;

        public OrderRepository(CNADbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(_ => _.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetOrders(Guid userId)
        {
            return await _context.Orders
                .Include(_ => _.Items)
                .Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<List<Order>> GetAllOrders(OrdersFilter filter)
        {
            var query = _context.Orders
                .Include(o => o.Items)
                .AsQueryable();

            if (filter.UserId.HasValue)
                query = query.Where(o => o.UserId == filter.UserId.Value);

            if (filter.OrderId.HasValue)
                query = query.Where(o => o.Id == filter.OrderId.Value);

            if (filter.OrderStatus.HasValue)
                query = query.Where(o => o.Status == filter.OrderStatus.Value);

            if (filter.IsPaid)
                query = query.Where(o => o.IsPaid);

            if (filter.MinCost.HasValue)
                query = query.Where(o => o.TotalAmount >= filter.MinCost.Value);

            if (filter.MaxCost.HasValue)
                query = query.Where(o => o.TotalAmount <= filter.MaxCost.Value);

            return await query.ToListAsync();
        }
    }
}
