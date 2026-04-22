using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.ProductVariantOperations;

public static class GetProductVariantById
{
    public record Query(Guid VariantId) : IRequest<ProductVariantResponse>;

    public class Handler : IRequestHandler<Query, ProductVariantResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public Handler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductVariantResponse> Handle(Query query, CancellationToken cancellationToken)
        {
            var variant = await _repository.GetVariantById(query.VariantId)
                ?? throw new VariantNotFoundException(query.VariantId);

            return _mapper.Map<ProductVariantResponse>(variant);
        }
    }
}
