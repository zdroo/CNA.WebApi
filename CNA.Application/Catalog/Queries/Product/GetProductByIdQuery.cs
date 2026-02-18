using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries.Product
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductResponse?>;

}
