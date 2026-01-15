using CSharpFunctionalExtensions;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Contracts.Persistence
{
    public interface IPurchaseRepository
    {
        Task<Result<IEnumerable<PurchaseDto>>> ReturnAllPuchasesAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<bool> ValidateExistsPurchaseByDocumentKey(int companyId, string documentId, CancellationToken cancellationToken);
        Task<Result> AddPurchaseAsync(int companyId, AddPurchaseDto purchase, CancellationToken cancellationToken);
    }
}
