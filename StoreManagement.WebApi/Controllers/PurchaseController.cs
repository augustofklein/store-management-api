using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Purchase.Command;
using StoreManagement.WebApi.Extensions;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    [Route("v{version:ApiVersion}/[controller]")]
    public class PurchaseController(IPurchaseRepository purchaseRepository, IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetPurchases(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
        {
            var response = await purchaseRepository.ReturnAllPuchasesAsync(User.GetCompanyId(), pageNumber, pageSize, cancellationToken);
            if (!response.Value.Any())
            {
                return NotFound();
            }

            return Ok(response.Value);
        }

        [HttpPost("preview-xml")]
        public async Task<IActionResult> PreviewPurchaseXml([FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            var command = new PreviewPurchaseXmlCommand(file, User.GetCompanyId());

            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddPurchase([FromBody] AddPurchaseCommand command, CancellationToken cancellationToken)
        {
            command.CompanyId = User.GetCompanyId();

            var result = await mediator.Send(command, cancellationToken);
            if(result.IsFailure)
                return BadRequest(result.Error);

            return Created();
        }
    }
}
