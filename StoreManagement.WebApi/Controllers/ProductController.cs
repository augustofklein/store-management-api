using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Product.Command;
using StoreManagement.Infrastructure.Repository.Product;
using StoreManagement.WebApi.InputModel;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    [Route("v{version:ApiVersion}/companies/{companyId}/[controller]")]
    public class ProductController(IMediator mediator, IProductRepository productRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts(int companyId, CancellationToken cancellationToken)
        {
            var response = await productRepository.GetProducts(companyId, cancellationToken);
            if (response.Value == null)
            {
                return NotFound();
            }

            return Ok(response.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(int companyId, [FromBody] AddProductInputModel inputModel, CancellationToken cancellationToken)
        {
            var command = AddProductCommand.CreateCommand(companyId, inputModel.SkuId, inputModel.Status, inputModel.Barcode, inputModel.Description, inputModel.Stock);

            var response = await mediator.Send(command, cancellationToken);
            if(response.IsFailure)
                return BadRequest(response.Error);

            return Created();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int companyId, int id, CancellationToken cancellationToken)
        {
            var command = RemoveProductCommand.CreateCommand(companyId, id);

            var response = await mediator.Send(command, cancellationToken);
            if(response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditProduct(int companyId, int id, [FromBody] EditProductInputModel product, CancellationToken cancellationToken)
        {
            var command = EditProductCommand.CreateCommand(companyId, id, product.Status, product.Description);

            var response = await mediator.Send(command, cancellationToken);

            if(response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }
    }
}
