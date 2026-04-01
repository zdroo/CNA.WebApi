using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Application.Models;
using CNA.Contracts.Responses;
using CNA.Domain.Filters;
using MediatR;

namespace CNA.Application.Catalog.ProductVariantOperations;

public static class GetProductVariants
{
    public record Query(
        string? ProductSlug,
        Guid? ProductId,
        Guid? CategoryId,
        string? Brand,
        PriceRange? PriceRange,
        string? SearchText,
        Dictionary<string, string>? Attributes,
        bool OnlyActive,
        bool OnlyInStock,
        bool Featured,
        ProductSortBy? SortBy,
        int Page,
        int PageSize) : IRequest<List<ProductVariantResponse>>;

    public class Handler : IRequestHandler<Query, List<ProductVariantResponse>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public Handler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductVariantResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<ProductVariantsFilter>(query);
            var variants = await _repository.GetFiltered(filter);

            return _mapper.Map<List<ProductVariantResponse>>(variants);
        }
    }
}