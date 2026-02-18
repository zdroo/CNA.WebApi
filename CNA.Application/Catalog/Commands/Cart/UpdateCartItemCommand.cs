using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Commands.Cart
{
    public record UpdateCartItemCommand(Guid CartItemId, int Quantity) : IRequest<CartItemResponse>;
}
