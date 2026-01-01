using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Product.Command;
using StoreManagement.WebApi.Extensions;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    [Route("v{version:ApiVersion}/[controller]")]
    public class ProductController(IMediator mediator, IProductRepository productRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command, CancellationToken cancellationToken)
        {
            command.CompanyId = User.GetCompanyId();

            var response = await mediator.Send(command, cancellationToken);
            if(response.IsFailure)
                return BadRequest(response.Error);

            return Created();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] EditProductCommand command, CancellationToken cancellationToken)
        {
            command.CompanyId = User.GetCompanyId();
            command.Id = id;

            var response = await mediator.Send(command, cancellationToken);

            if (response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var command = RemoveProductCommand.CreateCommand(User.GetCompanyId(), id);

            var response = await mediator.Send(command, cancellationToken);
            if(response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
        {
            var response = await productRepository.GetProductsAsync(User.GetCompanyId(), pageNumber, pageSize, cancellationToken);
            if (!response.Value.Any())
            {
                return NotFound();
            }

            return Ok(response.Value);
        }
    }
}
