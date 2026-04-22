using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using CNA.Domain.Catalog.Enums;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.ReviewOperations;

public static class AddReview
{
    public record Command(Guid UserId, Guid ProductVariantId, int Rating, string Comment) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            IReviewRepository reviewRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(Command command, CancellationToken cancellationToken)
        {
            var variant = await _productRepository.GetVariantById(command.ProductVariantId)
                ?? throw new VariantNotFoundException(command.ProductVariantId);

            var review = new Review(
                variant.Id,
                (RatingScore)command.Rating,
                command.Comment,
                command.UserId);

            await _reviewRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return review.Id;
        }
    }
}
