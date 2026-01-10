using CSharpFunctionalExtensions;
using StoreManagement.Application.Contracts.Persistence;

namespace StoreManagement.Application.Customer.Service
{
    public class CustomerService(ICustomerRepository custumerRepository) : ICustomerService
    {
        public async Task<Result> ValidateCustomerNotExistsAsync(int companyId, string documentNumber, CancellationToken cancellationToken)
        {
            if (await custumerRepository.VerifyCustomerByDocumentNumberExistAsync(companyId, documentNumber, cancellationToken))
                return Result.Failure("Customer already exists!");

            return Result.Success();
        }

        public async Task<Result> ValidateCustomerExistsByIdAsync(int companyId, int id, CancellationToken cancellationToken)
        {
            if (!await custumerRepository.VerifyCustomerByIdExistAsync(companyId, id, cancellationToken))
                return Result.Failure("Customer not exists!");

            return Result.Success();
        }
    }
}
