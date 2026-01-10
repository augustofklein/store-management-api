using CSharpFunctionalExtensions;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Purchase.Service
{
    public class PurchaseService(IProductRepository productRepository, ISupplierRepository supplierRepository) : IPurchaseService
    {
        public async Task<Result> ValidatePurchasePreviewAsync(int companyId, PurchasePreviewDto purchaseMapper, CancellationToken cancellationToken)
        {
            if(!await supplierRepository.ValidateSupplierExistsByIdentificationAsync(companyId, purchaseMapper.Supplier.Identification, cancellationToken))
                return Result.Failure($"Supplier with identification {purchaseMapper.Supplier.Identification} does not exist.");

            var productValidation = await productRepository
                .ValidateProductsByBarcodesAsync(
                    companyId,
                    [.. purchaseMapper.Products.Select(p => p.Barcode)],
                    cancellationToken);

            if (productValidation.IsFailure)
                return Result.Failure(productValidation.Error);

            return Result.Success();
        }
    }
}
