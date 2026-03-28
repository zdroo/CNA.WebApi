using MediatR;

namespace CNA.Application.Catalog.Commands.CategoriesCommands
{
    public record UpdateCategoryCommand(Guid CategoryId, string Name, string Slug) : IRequest;
}
