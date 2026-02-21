using MediatR;

namespace CNA.Application.Catalog.Commands.Cart
{
    public record CartCheckoutCommand(Guid UserId, List<Guid> CartItemIds) : IRequest;
}
