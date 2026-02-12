using MediatR;

namespace CNA.Application.Catalog.Queries.Cart
{
    public record GetCartByUserIdQuery(Guid UserId) : IRequest
    {
    }
}
