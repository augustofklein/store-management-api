using CSharpFunctionalExtensions;

namespace StoreManagement.Infrastructure.Repository.Product
{
    public interface IProductRepository
    {
        Task<Result> AddProduct(int companyId, string skuId, bool status, string barcode, string description, int stock, CancellationToken cancellationToken);
        Task<Result> RemoveProduct(int companyId, int id, CancellationToken cancellationToken);
        Task<Result> EditProduct(int companyId, int id, bool status, string description, CancellationToken cancellationToken);
    }
}
