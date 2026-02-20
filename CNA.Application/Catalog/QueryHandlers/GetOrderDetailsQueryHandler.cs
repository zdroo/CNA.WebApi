using CNA.Application.Catalog.Queries.Order;
using CNA.Application.Interfaces;
using CNA.Contracts.Enums;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderDetailsQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> Handle(GetOrderDetailsQuery query, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(query.OrderId)
                ?? throw new Exception("Order not found");

            var response = new OrderResponse(order.Id, order.TotalAmount, (OrderStatus)order.Status,
                order.Items.Select(i =>
                    new OrderItemResponse(
                        i.ProductVariantId,
                        i.Quantity,
                        i.Price,
                        i.Total
                        )
                    ).ToList());

            return response;
        }
    }
}
