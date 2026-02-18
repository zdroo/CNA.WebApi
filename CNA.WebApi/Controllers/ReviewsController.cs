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
        public Task<ActionResult> GetReviews()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<ActionResult> AddReview(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
