using CNA.Application.Catalog.Queries.Filters.Models;

namespace CNA.Application.Catalog.Queries.Filters
{
    public record OrdersFilter(
        Guid? UserId,
        Guid? OrderId,
        OrderStatus? OrderStatus,
        bool IsPaid,
        decimal? MinCost,
        decimal? MaxCost);
}
