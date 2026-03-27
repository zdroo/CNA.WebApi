using MediatR;

namespace CNA.Application.Catalog.Commands.Products
{
    public record DeleteProductCommand(Guid ProductId) : IRequest;
}
