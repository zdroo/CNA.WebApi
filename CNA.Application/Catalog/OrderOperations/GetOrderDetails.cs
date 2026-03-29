using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Enums;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.OrderOperations;

public static class GetOrderDetails
{
    public record Query(Guid OrderId) : IRequest<OrderResponse>;

    public class Handler : IRequestHandler<Query, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public Handler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(Query query, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(query.OrderId)
                        ?? throw new Exception("Order not found");

            return _mapper.Map<OrderResponse>(order);
        }
    }
}