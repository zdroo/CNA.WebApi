using CNA.Application.Catalog.CategoriesOperations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers.Seller
{
    [Route("api/seller/category")]
    [ApiController]
    public class SellerCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellerCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory(CreateCategory.Command request)
        {
            var id = await _mediator.Send(request);
            return Ok(id);
        }

        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId)
        {
            return Ok();
        }

        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            await _mediator.Send(new DeleteCategory.Command(categoryId));

            return NoContent();
        }
    }
}
