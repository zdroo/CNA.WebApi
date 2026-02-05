using CNA.Contracts.Requests.Products;
using MediatR;

namespace CNA.Application.Catalog.Commands
{
    public record UpdateProductCommand(Guid ProductId, UpdateProductRequest Request) : IRequest;
}
