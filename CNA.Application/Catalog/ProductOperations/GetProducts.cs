using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.ProductOperations;

public static class GetProducts
{
    public record Query(Guid? CategoryId,
        string? SearchText,
        bool IsFeatured,
        int PageSize = 12) : IRequest<List<ProductResponse>>;

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
            var products = await _repository.ListAllAsync();
            return _mapper.Map<List<ProductResponse>>(products);
        }
    }
}