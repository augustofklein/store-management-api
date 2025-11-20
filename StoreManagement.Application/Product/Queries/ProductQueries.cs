using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Product.Model;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Application.Product.Queries
{
    public class ProductQueries(AppDbContext dbContext) : IProductQueries
    {
        public async Task<Result<IEnumerable<ProductDto>>> GetProducts(int companyId, CancellationToken cancellationToken)
        {
            var result = await dbContext.Products
                .Where(p => p.CompanyId == companyId)
                .ToListAsync(cancellationToken);

            if(result.Count == 0)
                return new Result<IEnumerable<ProductDto>>();

            var products = result.Select(r => new ProductDto
            {
                Id = r.Id,
                SkuId = r.SkuId.Trim(),
                Status = r.Status,
                Barcode = r.Barcode,
                Description = r.Description,
                Stock = r.Stock
            });

            return Result.Success(products);
        }
    }
}
