using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using CNA.Domain.Filters;
using MediatR;

namespace CNA.Application.Catalog.AttributesOperations;

public static class GetAttributesFilter
{
    public record Query(Guid? CategoryId, Guid? ProductId) : IRequest<VariantFiltersResponse>;

    public class Handler : IRequestHandler<Query, VariantFiltersResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public Handler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<VariantFiltersResponse> Handle(Query query, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<AttributesFilter>(query);
            var variantFilters = await _productRepository.GetVariantFiltersAsync(filter);

            return _mapper.Map<VariantFiltersResponse>(variantFilters);
        }
    }
}