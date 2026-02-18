using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
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
    }
}
