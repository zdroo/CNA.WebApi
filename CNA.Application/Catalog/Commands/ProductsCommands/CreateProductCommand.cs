using CNA.Contracts.Requests;
using MediatR;

namespace CNA.Application.Catalog.Commands.Products
{
    public record CreateProductCommand(CreateProductRequest Request) : IRequest<Guid>;
}
