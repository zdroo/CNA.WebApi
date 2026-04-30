using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.FavoritesOperations
{
    public static class GetFavorites
    {
        public record Query(Guid? UserId, string? SessionId) : IRequest<List<FavoriteItemResponse>>;

        public class Handler : IRequestHandler<Query, List<FavoriteItemResponse>>
        {
            private readonly IFavoritesRepository _repository;
            private readonly IMapper _mapper;

            public Handler(IFavoritesRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<List<FavoriteItemResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = request.UserId.HasValue
                    ? await _repository.GetAllAsync(request.UserId.Value)
                    : await _repository.GetAllAsync(request.SessionId ?? string.Empty);

                return _mapper.Map<List<FavoriteItemResponse>>(items);
            }
        }
    }
}
