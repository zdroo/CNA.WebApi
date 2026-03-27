using MediatR;

namespace CNA.Application.Catalog.Commands.Categories
{
    public record DeleteCategoryCommand(Guid CategoryId) : IRequest;
}
