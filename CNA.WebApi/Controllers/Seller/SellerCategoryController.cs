using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers.Seller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellerCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IActionResult> GetCategories()
        {

        }
    }
}
