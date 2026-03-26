using CNA.Contracts.Requests.Filters.Models;

namespace CNA.Contracts.Requests.Filters
{
    public record OrdersFilterRequest(
        Guid? UserId,
        Guid? OrderId,
        OrderStatus? OrderStatus,
        bool IsPaid,
        decimal? MinCost,
        decimal? MaxCost);
}
