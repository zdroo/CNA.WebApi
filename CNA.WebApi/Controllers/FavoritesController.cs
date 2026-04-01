using CNA.Application.Catalog.FavoritesOperations;
using CNA.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private Guid CurrentUserId => _userContext.GetUserId();

        private readonly IUserContextService _userContext;
        private readonly IMediator _mediator;

        public FavoritesController(IUserContextService userContext, IMediator mediator)
        {
            _userContext = userContext;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(
            [FromBody] AddFavorite.Command request,
            CancellationToken cancellationToken)
        {
            request.UserId = CurrentUserId != Guid.Empty
                ? CurrentUserId 
                : null;

            await _mediator.Send(request, cancellationToken);

            return Ok();
        }
    }
}
