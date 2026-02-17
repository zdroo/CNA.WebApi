using CNA.Application.Catalog.Commands;
using CNA.Application.Catalog.Commands.Categories;
using CNA.Contracts.Requests.Categories;
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
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            var id = await _mediator.Send(new CreateCategoryCommand(request));
            return Ok(id);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            await _mediator.Send(new DeleteCategoryCommand(categoryId));

            return Ok();
        }
    }
}
