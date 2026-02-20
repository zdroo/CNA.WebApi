using CNA.Application.Catalog.Queries.Order;
using CNA.Application.Interfaces;
using CNA.Contracts.Enums;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersQueryHandler(
            IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderResponse>> Handle(GetOrdersQuery query,  CancellationToken cancellationToken)
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
                            )
                        ).ToList()))
                .ToList();

            return ordersResponse;
        }
    }
}
