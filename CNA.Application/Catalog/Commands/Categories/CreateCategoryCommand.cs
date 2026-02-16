using CNA.Contracts.Requests;
using CNA.Contracts.Requests.Categories;
using MediatR;

namespace CNA.Application.Catalog.Commands.Categories
{
    public record CreateCategoryCommand(CreateCategoryRequest Request) : IRequest<Guid>;
}
