using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Product.Command;
using StoreManagement.WebApi.Model;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("v{version:ApiVersion}/[controller]")]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [MapToApiVersion("1")]
        [HttpGet("all")]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var command = new GetProductsCommand();

            var response = await _mediator.Send(command, cancellationToken);

            if(response.IsFailure)
                return BadRequest(response.Error);

            return Ok(response.Value);
        }

        [MapToApiVersion("1")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductInputModel product, CancellationToken cancellationToken)
        {
            var command = new AddProductCommand(product.Id, product.Description, product.Stock);

            var response = await _mediator.Send(command, cancellationToken);

            if(response.IsFailure)
                return BadRequest(response.Error);

            return Ok();
        }

        [MapToApiVersion("1")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> AddProduct(int id, CancellationToken cancellationToken)
        {
            var command = new RemoveProductCommand(id);

            var response = await _mediator.Send(command, cancellationToken);

            if(response.IsFailure)
                return BadRequest(response.Error);

            return Ok();
        }

        [MapToApiVersion("1")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] EditProductInputModel product, CancellationToken cancellationToken)
        {
            var command = new EditProductCommand(id, product.Description);

            var response = await _mediator.Send(command, cancellationToken);

            if(response.IsFailure)
                return BadRequest(response.Error);

            return Ok();
        }
    }
}
