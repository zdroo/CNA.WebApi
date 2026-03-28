using CNA.Application.Catalog.Filters.Models;

namespace CNA.Application.Catalog.Filters
{
    public record OrdersFilter(
        Guid? UserId,
        Guid? OrderId,
        OrderStatus? OrderStatus,
        bool IsPaid,
        decimal? MinCost,
        decimal? MaxCost);
}
