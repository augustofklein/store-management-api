using CSharpFunctionalExtensions;
using StoreManagement.Application.Product.Model;

namespace StoreManagement.Application.Product.Queries
{
    public interface IProductQueries
    {
        Task<Result<IEnumerable<ProductDto>>> GetProducts(int companyId, CancellationToken cancellationToken);
    }
}
