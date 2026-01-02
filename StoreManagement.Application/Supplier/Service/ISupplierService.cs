using CSharpFunctionalExtensions;

namespace StoreManagement.Application.Supplier.Service
{
    public interface ISupplierService
    {
        Task<Result> ValidadeDeleteSupplierAsync(int companyId, int supplierId, CancellationToken cancellationToken);
        Task<Result> ValidateAddSupplierAsync(int companyId, string identification, CancellationToken cancellationToken);
    }
}
