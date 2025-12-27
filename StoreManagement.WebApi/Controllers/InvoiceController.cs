using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Invoice.Command;
using StoreManagement.Infrastructure.Repository.Invoice;
using StoreManagement.WebApi.Extensions;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    [Route("v{version:ApiVersion}/[controller]")]
    public class InvoiceController(IInvoiceRepository invoiceRepository, IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody] AddInvoiceCommand command, CancellationToken cancellationToken)
        {
            command.CompanyId = User.GetCompanyId();

            var response = await mediator.Send(command, cancellationToken);
            if (response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoices(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
        {
            var response = await invoiceRepository.ReturnAllInvoicesAsync(User.GetCompanyId(), pageNumber, pageSize, cancellationToken);
            if (response.Value == null)
            {
                return NotFound();
            }

            return Ok(response.Value);
        }
    }
}
