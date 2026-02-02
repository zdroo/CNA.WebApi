using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProductVariant()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<Guid>> GetProductVariantsByProductId(Guid productId)
        {
            throw new NotImplementedException();
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
