using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.ProductOperations;

public static class GetProducts
{
    public record Query : IRequest<List<ProductResponse>>
    {
        public string Category { get; init; } = default!;
        public Guid? CategoryId { get; init; }
        public string? SearchText { get; init; }
        public bool IsFeatured { get; init; }
        public int PageSize { get; init; } = 12;
    }

    public class Handler : IRequestHandler<Query, List<ProductResponse>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public Handler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var products = await _repository.ListByCategoryAsync(query.Category);
            return _mapper.Map<List<ProductResponse>>(products);
        }
    }
}