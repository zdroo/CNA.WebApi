using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries
{
    public record GetCategoriesQuery : IRequest<List<CategoryResponse>>;
}
