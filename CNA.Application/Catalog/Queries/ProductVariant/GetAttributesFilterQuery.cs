using CNA.Application.Catalog.Queries.Filters;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries.ProductVariant
{
    public record GetAttributesFilterQuery(AttributesFilter Filter) : IRequest<VariantFiltersResponse>;
}
