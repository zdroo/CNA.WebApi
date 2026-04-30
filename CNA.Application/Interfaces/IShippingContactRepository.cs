using CNA.Domain.Catalog.Entities.Localization;

namespace CNA.Application.Interfaces;

public interface IShippingContactRepository
{
    Task<List<ShippingContact>> GetByUserIdAsync(Guid userId);
    Task AddAsync(ShippingContact contact);
}
