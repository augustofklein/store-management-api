using CSharpFunctionalExtensions;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Purchase.Command;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Purchase.Service
{
    public class PurchaseService(IPurchaseRepository purchaseRepository, ICompanyRepository companyRepository, IProductRepository productRepository, ISupplierRepository supplierRepository) : IPurchaseService
    {
        public async Task<Result> EnrichAndValidatePurchasePreviewAsync(int companyId, PurchasePreviewDto purchaseMapper, CancellationToken cancellationToken)
        {
            if (purchaseMapper.Products.Count == 0)
                return Result.Failure<PurchasePreviewDto>("No products found in XML.");

            if (!await companyRepository.ExistsCompanyByDocumentNumberAsync(companyId, purchaseMapper.StoreDocumentNumber, cancellationToken))
                return Result.Failure($"Store with document number {purchaseMapper.StoreDocumentNumber} does not exist.");

            if (!await supplierRepository.ValidateSupplierExistsByDocumentNumberAsync(companyId, purchaseMapper.SupplierInformation.DocumentNumber, cancellationToken))
                return Result.Failure($"Supplier with document number {purchaseMapper.SupplierInformation.DocumentNumber} does not exist.");

            purchaseMapper.SupplierInformation.SupplierId = await supplierRepository.ReturnSupplierIdByDocumentNumber(companyId, purchaseMapper.SupplierInformation.DocumentNumber, cancellationToken);

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

        public async Task<Result> ValidateAddPurchaseAsync(AddPurchaseCommand command, CancellationToken cancellationToken)
        {
            if(command.Document.DocumentDate > DateTimeOffset.Now.DateTime)
                return Result.Failure("Purchase entry date cannot be in the future.");

            if(await purchaseRepository.ValidateExistsPurchaseByDocumentKey(command.CompanyId, command.Document.DocumentKey, cancellationToken))
                return Result.Failure($"A purchase with document key {command.Document.DocumentKey} already exists.");

            if (!await supplierRepository.ValidateSupplierExistsByIdAsync(command.CompanyId, command.SupplierId, cancellationToken))
                return Result.Failure($"Supplier with ID {command.SupplierId} does not exist.");

            var productValidation = await productRepository.VerifyArrayProductsExistAsync(command.CompanyId, command.Products.Select(x => x.ProductId), cancellationToken);
            if(productValidation.IsFailure)
                return Result.Failure(productValidation.Error);

            return Result.Success();
        }
    }
}
