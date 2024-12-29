using CSharpFunctionalExtensions;

namespace StoreManagement.Infrastructure.Repository.Product
{
    public interface IProductRepository
    {
        Task<Result> AddProduct(int id, string description, int stock, CancellationToken cancellationToken);
        Task<Result> RemoveProduct(int id, CancellationToken cancellationToken);
        Task<Result> EditProduct(int id, string description, CancellationToken cancellationToken);
    }
}
