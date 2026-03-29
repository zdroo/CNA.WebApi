using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.OrderOperations;

public static class GetOrders
{
    public record Query(Guid UserId) : IRequest<List<OrderResponse>>;

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
            var orders = await _orderRepository.GetOrders(query.UserId);
            return _mapper.Map<List<OrderResponse>>(orders);
        }
    }
}