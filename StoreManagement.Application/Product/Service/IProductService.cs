using CSharpFunctionalExtensions;

namespace StoreManagement.Application.Product.Service
{
    public interface IProductService
    {
        Task<Result> ValidadeAddProductAsync(int companyId, string skuId, CancellationToken cancellationToken);
        Task<Result> ValidateEditDeleteProduct(int companyId, int id, CancellationToken cancellationToken);
    }
}
