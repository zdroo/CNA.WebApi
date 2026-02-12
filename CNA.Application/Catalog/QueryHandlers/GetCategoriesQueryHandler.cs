using CNA.Application.Catalog.Queries;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return categories.Select(_ => new CategoryResponse(_.Name, _.IsActive)).ToList();
        }
    }
}
