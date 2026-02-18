using MediatR;

namespace CNA.Application.Catalog.Commands.Cart
{
    public record AddCartItemCommand(Guid UserId, Guid ProductVariantId) : IRequest;
}
