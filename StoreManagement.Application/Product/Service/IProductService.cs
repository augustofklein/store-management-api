using CSharpFunctionalExtensions;

namespace StoreManagement.Application.Product.Service
{
    public interface IProductService
    {
        Task<Result> ValidadeAddProductAsync(int companyId, string skuId, CancellationToken cancellationToken);
        Task<Result> ValidateEditProductAsync(int companyId, int productId, CancellationToken cancellationToken);
        Task<Result> ValidateDeleteProductAsync(int companyId, int productId, CancellationToken cancellationToken);
    }
}
