using CNA.Application.Interfaces;
using CNA.Contracts.Enums;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.OrderOperations;

public static class GetOrders
{
    public record Query(Guid UserId) : IRequest<List<OrderResponse>>;

    public class Handler : IRequestHandler<Query, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrders(query.UserId);

            var ordersResponse = orders.Select(o =>
                new OrderResponse(
                    o.Id,
                    o.TotalAmount,
                    (OrderStatus)o.Status,
                    o.Items.Select(i =>
                        new OrderItemResponse(
                            i.ProductVariantId,
                            i.Quantity,
                            i.Price,
                            i.Total
                        )).ToList()
                )).ToList();

            return ordersResponse;
        }
    }
}