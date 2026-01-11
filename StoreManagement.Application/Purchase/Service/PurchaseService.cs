using CSharpFunctionalExtensions;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Purchase.Service
{
    public class PurchaseService(ICompanyRepository companyRepository, IProductRepository productRepository, ISupplierRepository supplierRepository) : IPurchaseService
    {
        public async Task<Result> ValidatePurchasePreviewAsync(int companyId, PurchasePreviewDto purchaseMapper, CancellationToken cancellationToken)
        {
            if(!await companyRepository.ExistsCompanyByDocumentNumberAsync(companyId, purchaseMapper.Store.DocumentNumber, cancellationToken))
                return Result.Failure($"Store with document number {purchaseMapper.Store.DocumentNumber} does not exist.");

            if (!await supplierRepository.ValidateSupplierExistsByDocumentNumberAsync(companyId, purchaseMapper.Supplier.DocumentNumber, cancellationToken))
                return Result.Failure($"Supplier with document number {purchaseMapper.Supplier.DocumentNumber} does not exist.");

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
