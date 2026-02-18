using MediatR;

namespace CNA.Application.Catalog.Commands.Cart
{
    public record RemoveCartItemCommand(Guid CartItemId) : IRequest;
}
