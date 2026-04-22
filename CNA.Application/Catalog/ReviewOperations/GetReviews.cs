using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.ReviewOperations;

public static class GetReviews
{
    public record Query(Guid ProductVariantId) : IRequest<List<ReviewResponse>>;

    public class Handler : IRequestHandler<Query, List<ReviewResponse>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public Handler(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<List<ReviewResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetByProductVariantIdAsync(query.ProductVariantId);
            return _mapper.Map<List<ReviewResponse>>(reviews);
        }
    }
}
