using CNA.Application.Catalog.ReviewOperations;
using CNA.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private Guid CurrentUserId => _userContext.GetUserId();

        private readonly IUserContextService _userContext;
        private readonly IMediator _mediator;

        public ReviewsController(
            IUserContextService userContext,
            IMediator mediator)
        {
            _userContext = userContext;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(
            [FromQuery] Guid productVariantId,
            CancellationToken cancellationToken)
        {
            var reviews = await _mediator.Send(
                new GetReviews.Query(productVariantId),
                cancellationToken);

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddReview(
            [FromBody] AddReview.Command request,
            CancellationToken cancellationToken)
        {
            var command = request with { UserId = CurrentUserId };
            var id = await _mediator.Send(command, cancellationToken);
            return Ok(id);
        }
    }
}
