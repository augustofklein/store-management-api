using CSharpFunctionalExtensions;
using StoreManagement.Application.Contracts.Persistence;

namespace StoreManagement.Application.Supplier.Service
{
    public class SupplierService(ISupplierRepository supplierRepository) : ISupplierService
    {
        public async Task<Result> ValidadeDeleteSupplierAsync(int companyId, int supplierId, CancellationToken cancellationToken)
        {
            if(!await supplierRepository.ValidateSupplierExistsByIdAsync(companyId, supplierId, cancellationToken))
                return Result.Failure($"Not exists supplier with the specific Id.");

            if (await supplierRepository.ValidatePurchaseLinkedAsync(companyId, supplierId, cancellationToken))
                return Result.Failure("Is not possible to delete a supplier with purchase orders.");

            return Result.Success();
        }

        public async Task<Result> ValidateAddSupplierAsync(int companyId, string documentNumber, CancellationToken cancellationToken)
        {
            if(await supplierRepository.ValidateSupplierExistsByDocumentNumberAsync(companyId, documentNumber, cancellationToken))
                return Result.Failure("Supplier with the specific document number already exists.");

            return Result.Success();
        }
    }
}
