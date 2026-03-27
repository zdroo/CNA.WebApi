using CNA.Application.Catalog.Commands.Categories;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Categories
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(DeleteCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByIdAsync(command.CategoryId)
                ?? throw new Exception("Category not found");

            await _categoryRepository.DeleteCategoryAsync(category);
        }
    }
}
