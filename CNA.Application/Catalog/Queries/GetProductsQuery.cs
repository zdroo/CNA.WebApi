using CNA.Application.Catalog.Queries.Filters;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries
{
    public record GetProductsQuery(ProductsFilter Filter) : IRequest<List<ProductResponse>>;
}
