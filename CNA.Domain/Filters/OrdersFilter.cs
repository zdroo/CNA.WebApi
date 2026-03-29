using CNA.Domain.Catalog.Enums;

namespace CNA.Domain.Filters
{
    public record OrdersFilter(
        Guid? UserId,
        Guid? OrderId,
        OrderStatus? OrderStatus,
        bool IsPaid,
        decimal? MinCost,
        decimal? MaxCost);
}
