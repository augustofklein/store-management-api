using CSharpFunctionalExtensions;
using StoreManagement.Application.Contracts.Persistence;
using System.ComponentModel.Design;
using System.Threading;

namespace StoreManagement.Application.Product.Service
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public async Task<Result> ValidadeAddProductAsync(int companyId, string skuId, CancellationToken cancellationToken)
        {
            if (await productRepository.VerifyProductBySkuIdExistAsync(companyId, skuId, cancellationToken))
                return Result.Failure("Product already exist!");

            return Result.Success();
        }

        public async Task<Result> ValidateEditProductAsync(int companyId, int productId, CancellationToken cancellationToken)
        {
            if (!await productRepository.VerifyProductByIdExistAsync(companyId, productId, cancellationToken))
                return Result.Failure("Product not exist!");

            return Result.Success();
        }

        public async Task<Result> ValidateDeleteProductAsync(int companyId, int productId, CancellationToken cancellationToken)
        {
            if (!await productRepository.VerifyProductByIdExistAsync(companyId, productId, cancellationToken))
                return Result.Failure("Product not exist!");

            if(await productRepository.ProductExistsInInvoicesAsync(companyId, productId, cancellationToken))
                return Result.Failure("Product cannot be deleted because it is associated with existing invoices.");

            if(await productRepository.ProductExistsInPurchasesAsync(companyId, productId, cancellationToken))
                return Result.Failure("Product cannot be deleted because it is associated with existing purchases.");

            return Result.Success();
        }
    }
}
