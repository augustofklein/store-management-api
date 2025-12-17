using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Product.Command;
using StoreManagement.WebApi.Extensions;
using StoreManagement.WebApi.InputModel.Product;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    [Route("v{version:ApiVersion}/[controller]")]
    public class ProductController(IMediator mediator, IProductRepository productRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var response = await productRepository.GetProducts(User.GetCompanyId(), cancellationToken);
            if (response.Value == null)
            {
                return NotFound();
            }

            return Ok(response.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductInputModel inputModel, CancellationToken cancellationToken)
        {
            var command = AddProductCommand.CreateCommand(User.GetCompanyId(), inputModel.SkuId, inputModel.Status, inputModel.Barcode, inputModel.Description, inputModel.Stock);

            var response = await mediator.Send(command, cancellationToken);
            if(response.IsFailure)
                return BadRequest(response.Error);

            return Created();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id, CancellationToken cancellationToken)
        {
            var command = RemoveProductCommand.CreateCommand(User.GetCompanyId(), id);

            var response = await mediator.Send(command, cancellationToken);
            if(response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] EditProductInputModel product, CancellationToken cancellationToken)
        {
            var command = EditProductCommand.CreateCommand(User.GetCompanyId(), id, product.Status, product.Description);

            var response = await mediator.Send(command, cancellationToken);

            if(response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }
    }
}
