using CSharpFunctionalExtensions;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Purchase.Service
{
    public interface IPurchaseService
    {
        Task<Result> ValidatePurchasePreviewAsync(int companyId, PurchasePreviewDto purchaseMapper, CancellationToken cancellationToken);
    }
}
