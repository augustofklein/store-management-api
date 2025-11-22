using CSharpFunctionalExtensions;
using StoreManagement.Infrastructure.Repository.Product;

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

        public async Task<Result> ValidateEditDeleteProduct(int companyId, int id, CancellationToken cancellationToken)
        {
            if (!await productRepository.VerifyProductByIdExistAsync(companyId, id, cancellationToken))
                return Result.Failure("Product not exist!");

            return Result.Success();
        }
    }
}
