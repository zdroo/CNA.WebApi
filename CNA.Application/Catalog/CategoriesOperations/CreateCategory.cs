using CNA.Application.Interfaces;
using CNA.Contracts.Requests.Categories;
using CNA.Domain.Catalog.Entities;
using MediatR;

namespace CNA.Application.Catalog.CategoriesOperations
{
    public static class CreateCategory
    {
        public record Command(CreateCategoryRequest Request) : IRequest<Guid>;

        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly ICategoryRepository _categoryRepository;
            public Handler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<Guid> Handle(Command command, CancellationToken cancellationToken = default)
            {
                var r = command.Request;
                var category = new Category(r.Name, r.Slug);

                await _categoryRepository.AddCategoryAsync(category);
                return category.Id;
            }
        }
    }
}
