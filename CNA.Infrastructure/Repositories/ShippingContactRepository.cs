using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities.Localization;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories;

public class ShippingContactRepository : IShippingContactRepository
{
    private readonly CNADbContext _context;

    public ShippingContactRepository(CNADbContext context)
    {
        _context = context;
    }

    public async Task<List<ShippingContact>> GetByUserIdAsync(Guid userId)
        => await _context.ShippingContacts
            .Include(sc => sc.Address)
            .Where(sc => sc.UserId == userId)
            .ToListAsync();

    public Task AddAsync(ShippingContact contact)
    {
        if (contact.Address != null)
            _context.Addresses.Add(contact.Address);
        _context.ShippingContacts.Add(contact);
        return Task.CompletedTask;
    }
}
