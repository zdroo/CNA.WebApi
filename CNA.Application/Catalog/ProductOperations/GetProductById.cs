using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using CNA.Domain.Catalog.Entities;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.ProductOperations;

public static class GetProductById
{
    public record Query(Guid ProductId) : IRequest<ProductResponse?>;

    public class Handler : IRequestHandler<Query, ProductResponse?>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public Handler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponse?> Handle(Query query, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(query.ProductId)
                ?? throw new ProductNotFoundException(query.ProductId);

            return _mapper.Map<ProductResponse>(product);
        }
    }
}