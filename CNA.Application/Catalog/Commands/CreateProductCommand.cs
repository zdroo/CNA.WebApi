using CNA.Contracts.Requests;
using MediatR;

namespace CNA.Application.Catalog.Commands
{
    public record CreateProductCommand(CreateProductRequest Request) : IRequest<Guid>;
}
