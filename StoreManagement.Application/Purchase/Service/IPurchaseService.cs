using CSharpFunctionalExtensions;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Purchase.Service
{
    public interface IPurchaseService
    {
        Task<Result> EnrichAndValidatePurchasePreviewAsync(int companyId, PurchasePreviewDto purchaseMapper, CancellationToken cancellationToken);
    }
}
