using CSharpFunctionalExtensions;
using StoreManagement.Application.Customer.Model.Dto;

namespace StoreManagement.Application.Contracts.Persistence
{
    public interface ICustomerRepository
    {
        Task<bool> VerifyCustomerByDocumentNumberExistAsync(int companyId, string documentNumber, CancellationToken cancellationToken);
        Task<bool> VerifyCustomerByIdExistAsync(int companyId, int id, CancellationToken cancellationToken);
        Task<Result<IEnumerable<CustomerDto>>> ReturnAllCustomersAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<bool> AddCustomerAsync(int companyId, CustomerDto customer, CancellationToken cancellationToken);
        Task<bool> UpdateCustomerAsync(int companyId, CustomerDto customer, CancellationToken cancellationToken);
        Task<bool> DeleteCustomerAsync(int companyId, int customerId, CancellationToken cancellationToken);
    }
}
