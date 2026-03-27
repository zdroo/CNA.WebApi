using MediatR;

namespace CNA.Application.Catalog.Commands.Cart
{
    public record ClearCartCommand(Guid UserId) : IRequest;
}
