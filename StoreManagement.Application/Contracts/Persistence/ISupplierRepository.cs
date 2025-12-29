using CSharpFunctionalExtensions;
using StoreManagement.Application.Supplier.Model;

namespace StoreManagement.Application.Contracts.Persistence
{
    public interface ISupplierRepository
    {
        Task<Result<IEnumerable<SupplierDto>>> ReturnAllSuppliersAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
