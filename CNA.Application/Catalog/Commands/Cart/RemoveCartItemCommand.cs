using MediatR;

namespace CNA.Application.Catalog.Commands.Cart
{
    public record RemoveCartItemCommand(Guid UserId, Guid CartItemId) : IRequest;
}
