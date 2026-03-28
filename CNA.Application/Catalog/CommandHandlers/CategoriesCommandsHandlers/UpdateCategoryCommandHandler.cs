using CNA.Application.Catalog.Commands.CategoriesCommands;
using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.CategoriesCommandsHandlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            UpdateCategoryCommand command,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(command.Name) && string.IsNullOrWhiteSpace(command.Slug))
                return;

            var category = await _categoryRepository.GetByIdAsync(command.CategoryId)
                ?? throw new CategoryNotFoundException(command.CategoryId);

            category.UpdateCategory(command.Name, command.Slug);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
