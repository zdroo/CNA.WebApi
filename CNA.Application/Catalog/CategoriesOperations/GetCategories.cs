using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.CategoriesOperations;

public static class GetCategories
{
    public record Query() : IRequest<List<CategoryResponse>>;

    public class Handler : IRequestHandler<Query, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public Handler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            return _mapper.Map<List<CategoryResponse>>(categories);
        }
    }
}