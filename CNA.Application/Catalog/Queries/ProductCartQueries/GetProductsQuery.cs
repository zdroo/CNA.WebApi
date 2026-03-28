using CNA.Application.Catalog.Filters;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries.Product
{
    public record GetProductsQuery(ProductsFilter Filter) : IRequest<List<ProductResponse>>;
}
