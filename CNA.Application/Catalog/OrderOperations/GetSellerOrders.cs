using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using CNA.Domain.Catalog.Enums;
using CNA.Domain.Filters;
using MediatR;

namespace CNA.Application.Catalog.OrderOperations;

public static class GetSellerOrders
{
    public record Query(
        Guid? UserId,
        Guid? OrderId,
        OrderStatus? OrderStatus,
        bool IsPaid,
        decimal? MinCost,
        decimal? MaxCost) : IRequest<List<OrderResponse>>;

    public class Handler : IRequestHandler<Query, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public Handler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var filter = new OrdersFilter(
                query.UserId,
                query.OrderId,
                query.OrderStatus,
                query.IsPaid,
                query.MinCost,
                query.MaxCost);

            var orders = await _orderRepository.GetAllOrders(filter);
            return _mapper.Map<List<OrderResponse>>(orders);
        }
    }
}
