using CSharpFunctionalExtensions;

namespace StoreManagement.Application.Customer.Service
{
    public interface ICustomerService
    {
        Task<Result> ValidateCustomerNotExistsAsync(int companyId, string documentNumber, CancellationToken cancellationToken);
        Task<Result> ValidateCustomerExistsByIdAsync(int companyId, int id, CancellationToken cancellationToken);
    }
}
