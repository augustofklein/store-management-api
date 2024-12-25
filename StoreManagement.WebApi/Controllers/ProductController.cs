using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Product.Command;

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
    }
}
