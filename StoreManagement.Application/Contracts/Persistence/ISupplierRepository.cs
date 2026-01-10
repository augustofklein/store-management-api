using CSharpFunctionalExtensions;
using StoreManagement.Application.Supplier.Model;

namespace StoreManagement.Application.Contracts.Persistence
{
    public interface ISupplierRepository
    {
        Task<Result<IEnumerable<SupplierDto>>> ReturnAllSuppliersAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<bool> ValidatePurchaseLinkedAsync(int companyId, int supplierId, CancellationToken cancellationToken);
        Task<Result> DeleteSupplierAsync(int companyId, int supplierId, CancellationToken cancellationToken);
        Task<bool> ValidateSupplierExistsByIdAsync(int companyId, int supplierId, CancellationToken cancellationToken);
        Task<Result> AddSupplierAsync(int companyId, AddSupplierDto addSupplier, CancellationToken cancellationToken);
        Task<bool> ValidateSupplierExistsByIdentificationAsync(int companyId, string identification, CancellationToken cancellationToken);
    }
}
