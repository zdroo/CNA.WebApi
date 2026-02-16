using CNA.Application.Catalog.Commands.Categories;
using CNA.Application.Interfaces;
using CNA.Domain.Catalog;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Categories
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(CreateCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var r = command.Request;
            var category = new Category(r.Name, r.Slug);

            await _categoryRepository.AddCategoryAsync(category);
            return category.Id;
        }
    }
}
