using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Application.Models;
using CNA.Contracts.Responses;
using CNA.Domain.Filters;
using MediatR;

namespace CNA.Application.Catalog.ProductVariantOperations;

public static class GetProductVariants
{
    public record Query : IRequest<List<ProductVariantResponse>>
    {
        public string? ProductSlug { get; init; }
        public string? VariantSlug { get; init; }
        public Guid? ProductId { get; init; }
        public Guid? CategoryId { get; init; }
        public string? Brand { get; init; }
        public PriceRange? PriceRange { get; init; }
        public string? SearchText { get; init; }
        public Dictionary<string, string>? Attributes { get; init; }
        public bool OnlyActive { get; init; }
        public bool OnlyInStock { get; init; }
        public bool Featured { get; init; }
        public ProductSortBy? SortBy { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 12;
    }

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