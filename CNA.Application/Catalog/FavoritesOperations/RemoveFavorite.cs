using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.FavoritesOperations
{
    public static class RemoveFavorite
    {
        public record Command(Guid FavoriteItemId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IFavoritesRepository _favoritesRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IFavoritesRepository favoritesRepository, IUnitOfWork unitOfWork)
            {
                _favoritesRepository = favoritesRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var favorite = await _favoritesRepository.GetByIdAsync(request.FavoriteItemId)
                    ?? throw new FavoriteItemNotFoundException(request.FavoriteItemId);

                await _favoritesRepository.RemoveAsync(favorite);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
