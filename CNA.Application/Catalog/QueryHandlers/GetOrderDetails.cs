using CNA.Application.Interfaces;
using CNA.Contracts.Enums;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Order;

public static class GetOrderDetails
{
    public record Query(Guid OrderId) : IRequest<OrderResponse>;

    public class Handler : IRequestHandler<Query, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> Handle(Query query, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(query.OrderId)
                        ?? throw new Exception("Order not found");

            var response = new OrderResponse(
                order.Id,
                order.TotalAmount,
                (OrderStatus)order.Status,
                order.Items.Select(i => new OrderItemResponse(
                    i.ProductVariantId,
                    i.Quantity,
                    i.Price,
                    i.Total
                )).ToList()
            );

            return response;
        }
    }
}