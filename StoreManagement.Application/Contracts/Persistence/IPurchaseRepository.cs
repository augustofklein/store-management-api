using CSharpFunctionalExtensions;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Contracts.Persistence
{
    public interface IPurchaseRepository
    {
        Task<Result<IEnumerable<PurchaseDto>>> ReturnAllPuchasesAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
