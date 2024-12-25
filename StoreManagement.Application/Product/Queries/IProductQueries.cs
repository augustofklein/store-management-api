using CSharpFunctionalExtensions;
using StoreManagement.Application.Product.Model;

namespace StoreManagement.Application.Product.Queries
{
    public interface IProductQueries
    {
        Task<Result<IEnumerable<ProductDto>>> GetProducts(CancellationToken cancellationToken);
    }
}
