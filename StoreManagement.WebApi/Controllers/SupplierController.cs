using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Infrastructure.Repository.Customer;
using StoreManagement.WebApi.Extensions;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    [Route("v{version:ApiVersion}/[controller]")]
    public class SupplierController(ISupplierRepository supplierRepository) : ControllerBase
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
    }
}
