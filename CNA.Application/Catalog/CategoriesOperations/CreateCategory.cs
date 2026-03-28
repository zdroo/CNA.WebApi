using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using MediatR;

namespace CNA.Application.Catalog.CategoriesOperations
{
    public static class CreateCategory
    {
        public record Command(string Name, string Slug, Guid? ParentCategoryId) : IRequest<Guid>;

        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly ICategoryRepository _categoryRepository;
            public Handler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<Guid> Handle(Command command, CancellationToken cancellationToken = default)
            {
                var category = new Category(command.Name, command.Slug);

                await _categoryRepository.AddCategoryAsync(category);
                return category.Id;
            }
        }
    }
}
