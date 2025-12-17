using CSharpFunctionalExtensions;

namespace StoreManagement.Application.Customer.Service
{
    public interface ICustomerService
    {
        Task<Result> ValidateAddCustomerAsync(int companyId, string identification, CancellationToken cancellationToken);
        Task<Result> ValidateEditCustomerAsync(int companyId, string identification, CancellationToken cancellationToken);
    }
}
