using MediatR;
using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;

namespace CNA.Application.Catalog.CategoriesOperations;

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
                ?? throw new CategoryNotFoundException(command.CategoryId);

            await _categoryRepository.DeleteCategoryAsync(category);
        }
    }
}