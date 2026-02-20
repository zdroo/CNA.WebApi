using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries.Order
{
    public record GetOrderDetailsQuery(Guid OrderId) : IRequest<OrderResponse>;
}
