using MediatR;

namespace CNA.Application.Catalog.Commands
{
    public record DeleteProductCommand(Guid productId) : IRequest;
}
