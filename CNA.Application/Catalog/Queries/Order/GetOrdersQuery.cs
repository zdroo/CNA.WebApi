using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries.Order
{
    public record GetOrdersQuery(Guid UserId) : IRequest<List<OrderResponse>>;
}
