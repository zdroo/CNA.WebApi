using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
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
