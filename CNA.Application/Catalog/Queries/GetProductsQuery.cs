using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries
{
    public record GetProductsQuery : IRequest<List<ProductResponse>>;;
}
