using Azure.Core;
using CNA.Application.Catalog.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace CNA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductVariantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProductVariant()
        {
            throw new NotImplementedException();
        }
        

        [HttpGet]
        public async Task<ActionResult<Guid>> GetVariants(
        Guid productId,
        CancellationToken cancellationToken)
        {
            var variantId = await _mediator.Send(
                new GetProductVariantsQuery(productId),
                cancellationToken);

            return Ok(variantId);
        }

        [HttpGet]
        public async Task<ActionResult<Guid>> GetProductVariantsById(Guid productVariantId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteProductVariant(Guid productVariantId)
        {
            throw new NotImplementedException();
        }
    }
}
