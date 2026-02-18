using CNA.Contracts.Enums;

namespace CNA.Contracts.Responses
{
    public record OrderResponse(Guid OrderId, decimal TotalAmount, OrderStatus Status, List<OrderItemResponse> Items);
}
