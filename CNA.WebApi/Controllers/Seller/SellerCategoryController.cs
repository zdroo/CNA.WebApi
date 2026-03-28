using CNA.Application.Catalog.CategoriesOperations;
using CNA.Contracts.Requests.Categories;
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
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            var id = await _mediator.Send(new CreateCategory.Command(request));
            return Ok(id);
        }

        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId)
        {
            return Ok();
        }

        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            await _mediator.Send(new DeleteCategory.Command(categoryId));

            return NoContent();
        }
    }
}
