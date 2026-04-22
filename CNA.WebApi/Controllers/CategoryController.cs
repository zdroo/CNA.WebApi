using CNA.Application.Catalog.CategoriesOperations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _mediator.Send(new GetCategories.Query(), cancellationToken);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromBody] CreateCategory.Command request,
            CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(request, cancellationToken);
            return Ok(id);
        }

        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid categoryId,
            [FromBody] UpdateCategory.Command request,
            CancellationToken cancellationToken)
        {
            request = request with { CategoryId = categoryId };
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCategory.Command(categoryId), cancellationToken);
            return NoContent();
        }
    }
}
