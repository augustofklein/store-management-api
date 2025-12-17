using CSharpFunctionalExtensions;
using StoreManagement.Application.Contracts.Persistence;

namespace StoreManagement.Application.Customer.Service
{
    public class CustomerService(ICustomerRepository custumerRepository) : ICustomerService
    {
        public async Task<Result> ValidateAddCustomerAsync(int companyId, string identification, CancellationToken cancellationToken)
        {
            if (await custumerRepository.VerifyCustomerByIdentificationExistAsync(companyId, identification, cancellationToken))
                return Result.Failure("Customer already exists!");

            return Result.Success();
        }

        public async Task<Result> ValidateEditCustomerAsync(int companyId, string identification, CancellationToken cancellationToken)
        {
            if (!await custumerRepository.VerifyCustomerByIdentificationExistAsync(companyId, identification, cancellationToken))
                return Result.Failure("Customer not exists!");

            return Result.Success();
        }
    }
}
