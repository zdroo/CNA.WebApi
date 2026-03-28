using MediatR;
using CNA.Application.Interfaces;

namespace CNA.Application.Catalog.Categories;

public static class DeleteCategory
{
    public record Command(Guid CategoryId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly ICategoryRepository _categoryRepository;

        public Handler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(command.CategoryId)
                ?? throw new Exception("Category not found");

            await _categoryRepository.DeleteCategoryAsync(category);
        }
    }
}