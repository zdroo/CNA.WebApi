using CNA.Application.Catalog.Queries.Filters;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries
{
    public record GetProductVariantsQuery(Guid ProductId) : IRequest<List<ProductVariantResponse?>>;

}
