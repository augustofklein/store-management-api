using CSharpFunctionalExtensions;
using StoreManagement.Application.Product.Queries;

namespace StoreManagement.Application.Product.Service
{
    public class ProductService(IProductQueries productQueries) : IProductService
    {
        public async Task<Result> ValidadeAddProductAsync(int companyId, string skuId, CancellationToken cancellationToken)
        {
            if (await productQueries.VerifyProductBySkuIdExistAsync(companyId, skuId, cancellationToken))
                return Result.Failure("Product already exist!");

            return Result.Success();
        }

        public async Task<Result> ValidateEditDeleteProduct(int companyId, int id, CancellationToken cancellationToken)
        {
            if (!await productQueries.VerifyProductByIdExistAsync(companyId, id, cancellationToken))
                return Result.Failure("Product not exist!");

            return Result.Success();
        }
    }
}
