using CNA.Contracts.Requests.Products;
using MediatR;

namespace CNA.Application.Catalog.Commands.Products
{
    public record UpdateProductCommand(Guid ProductId, UpdateProductRequest Request) : IRequest;
}
