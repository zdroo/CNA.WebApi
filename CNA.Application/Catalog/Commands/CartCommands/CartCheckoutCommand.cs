using MediatR;

namespace CNA.Application.Catalog.Commands.Cart
{
    public record CartCheckoutCommand(Guid UserId, Guid ShippingContactId, List<Guid> CartItemIds) : IRequest;
}
