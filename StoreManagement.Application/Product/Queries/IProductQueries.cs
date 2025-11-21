using CSharpFunctionalExtensions;
using StoreManagement.Application.Product.Model;

namespace StoreManagement.Application.Product.Queries
{
    public interface IProductQueries
    {
        Task<Result<IEnumerable<ProductDto>>> GetProducts(int companyId, CancellationToken cancellationToken);
        Task<bool> VerifyProductByIdExistAsync(int companyId, int id, CancellationToken cancellationToken);
        Task<bool> VerifyProductBySkuIdExistAsync(int companyId, string skuId, CancellationToken cancellationToken);
    }
}
