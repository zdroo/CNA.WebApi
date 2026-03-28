using MediatR;
using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;

namespace CNA.Application.Catalog.CategoriesOperations;

public static class UpdateCategory
{
    public record Command(Guid CategoryId, string? Name, string? Slug) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Name) && string.IsNullOrWhiteSpace(command.Slug))
                return;

            var category = await _categoryRepository.GetByIdAsync(command.CategoryId)
                ?? throw new CategoryNotFoundException(command.CategoryId);

            category.UpdateCategory(command.Name, command.Slug);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}