using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using MediatR;

namespace CNA.Application.Catalog.FavoritesOperations
{
    public static class AddFavorite
    {
        public record Command : IRequest
        {
            public Guid ProductVariantId { get; init; }
            public Guid? UserId { get; set; }
            public string? SessionId { get; init; }

            public Command(Guid productVariantId, Guid? userId = null, string? sessionId = null)
            {
                ProductVariantId = productVariantId;
                UserId = userId;
                SessionId = sessionId;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IFavoritesRepository _favoriteRepository;

            public Handler(IFavoritesRepository favoriteRepository)
            {
                _favoriteRepository = favoriteRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.UserId == null && string.IsNullOrWhiteSpace(request.SessionId))
                {
                    throw new InvalidDataException("Either UserId or SessionId must be provided.");
                }

                var favorite = new FavoriteItem(
                    productVariantId: request.ProductVariantId,
                    userId: request.UserId,
                    sessionId: request.SessionId
                );

                await _favoriteRepository.AddAsync(favorite);
            }
        }
    }
}
