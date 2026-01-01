using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Supplier.Command;
using StoreManagement.WebApi.Extensions;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    [Route("v{version:ApiVersion}/[controller]")]
    public class SupplierController(ISupplierRepository supplierRepository, IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSuppliers(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
        {
            var response = await supplierRepository.ReturnAllSuppliersAsync(User.GetCompanyId(), pageNumber, pageSize, cancellationToken);
            if (!response.Value.Any())
            {
                return NotFound();
            }

            return Ok(response.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteSupplierCommand(User.GetCompanyId(), id);

            var response = await mediator.Send(command, cancellationToken);
            if (response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }

    }
}
