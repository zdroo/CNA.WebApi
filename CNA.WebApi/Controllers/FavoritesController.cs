using CNA.Application.Catalog.FavoritesOperations;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private Guid? CurrentUserId => _userContext.IsAuthenticated() ? _userContext.GetUserId() : null;

        private readonly IUserContextService _userContext;
        private readonly IMediator _mediator;

        public FavoritesController(IUserContextService userContext, IMediator mediator)
        {
            _userContext = userContext;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<FavoriteItemResponse>>> GetFavorites(
            [FromQuery] string? sessionId,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetFavorites.Query(CurrentUserId, sessionId),
                cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(
            [FromBody] AddFavorite.Command request,
            CancellationToken cancellationToken)
        {
            request.UserId = CurrentUserId;

            var favoriteId = await _mediator.Send(request, cancellationToken);

            return Ok(favoriteId);
        }

        [HttpDelete("{favoriteItemId:guid}")]
        public async Task<IActionResult> RemoveFavorite(
            [FromRoute] Guid favoriteItemId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new RemoveFavorite.Command(favoriteItemId), cancellationToken);
            return NoContent();
        }
    }
}
