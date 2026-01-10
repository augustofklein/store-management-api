using CSharpFunctionalExtensions;
using StoreManagement.Application.Product.Model;
using StoreManagement.Domain.Enums;

namespace StoreManagement.Application.Contracts.Persistence
{
    public interface IProductRepository
    {
        Task<Result> AddProductAsync(int companyId, string skuId, bool status, string barcode, string description, int stock, decimal price, CancellationToken cancellationToken);
        Task<Result> DeleteProductAsync(int companyId, int id, CancellationToken cancellationToken);
        Task<Result> EditProductAsync(int companyId, int id, bool status, string description, CancellationToken cancellationToken);
        Task<Result<IEnumerable<ProductDto>>> GetProductsAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<bool> VerifyProductByIdExistAsync(int companyId, int id, CancellationToken cancellationToken);
        Task<bool> VerifyProductBySkuIdExistAsync(int companyId, string skuId, CancellationToken cancellationToken);
        Task<Result> VerifyArrayProductsExistAsync(int companyId, IEnumerable<int> productIds, CancellationToken cancellationToken);
        Task<Result> ValidateProductsByBarcodesAsync(int companyId, IEnumerable<string> barcodeProducts, CancellationToken cancellationToken);
        Task AddProductMovementArrayAsync(ProductMovementEnum movementType, DateTime movementDate, List<AddProductMovementDto> items, CancellationToken cancellationToken);
        Task UpdateProductStockArrayAsync(ProductMovementEnum movementType, List<UpdateProductStockDto> items, CancellationToken cancellationToken);
    }
}
