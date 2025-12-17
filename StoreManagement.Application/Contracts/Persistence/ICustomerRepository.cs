using CSharpFunctionalExtensions;
using StoreManagement.Application.Customer.Model.Dto;

namespace StoreManagement.Application.Contracts.Persistence
{
    public interface ICustomerRepository
    {
        Task<bool> VerifyCustomerByIdentificationExistAsync(int companyId, string identification, CancellationToken cancellationToken);
        Task<Result<IEnumerable<CustomerDto>>> ReturnAllCustomersAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<bool> AddCustomerAsync(int companyId, CustomerDto customer, CancellationToken cancellationToken);
    }
}
