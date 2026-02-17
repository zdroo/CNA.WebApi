using MediatR;

namespace CNA.Application.Catalog.Commands.Cart
{
    public record UpdateCartItemCommand(Guid UserId, Guid CartItemId, int Quantity) : IRequest;
}
