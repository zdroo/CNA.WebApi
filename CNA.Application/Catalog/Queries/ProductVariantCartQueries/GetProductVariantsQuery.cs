using CNA.Application.Catalog.Filters;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries.ProductVariant
{
    public record GetProductVariantsQuery(ProductVariantsFilter Filter) : IRequest<List<ProductVariantResponse>>;
}
