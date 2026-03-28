using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Categories;

public static class GetCategories
{
    public record Query() : IRequest<List<CategoryResponse>>;

    public class Handler : IRequestHandler<Query, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public Handler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return categories
                .Select(c => new CategoryResponse(c.Name, c.IsActive))
                .ToList();
        }
    }
}