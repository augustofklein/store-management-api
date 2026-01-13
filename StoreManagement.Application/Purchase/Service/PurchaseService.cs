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

            var products = await productRepository.ReturnProductsByBarcodeAsync(companyId, [.. purchaseMapper.Products.Select(p => p.Barcode)], cancellationToken);

            var productMap = products.ToDictionary(p => p.Barcode, p => new { p.Id, p.SkuId });

            foreach (var item in purchaseMapper.Products)
            {
                if (productMap.TryGetValue(item.Barcode, out var product))
                {
                    item.Id = product.Id;
                    item.SkuId = product.SkuId;
                }
                else
                {
                    item.ValidationMessage = "Product not found for the given barcode.";
                }
            }

            if (purchaseMapper.Products.Any(p => !p.ProductFound))
                return Result.Failure("Some products could not be resolved.");

            return Result.Success();
        }
    }
}
