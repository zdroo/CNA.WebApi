using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries.Categories
{
    public record GetCategoriesQuery : IRequest<List<CategoryResponse>>;
}
