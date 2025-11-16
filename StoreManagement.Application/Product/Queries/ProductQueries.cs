using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Product.Model;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Application.Product.Queries
{
    public class ProductQueries(AppDbContext dbContext) : IProductQueries
    {
        public async Task<Result<IEnumerable<ProductDto>>> GetProducts(CancellationToken cancellationToken)
        {
            var result = await dbContext.Products.AsNoTracking().ToListAsync(cancellationToken);

            var products = result.Select(r => new ProductDto
            {
                Id = r.Id,
                Barcode = r.Barcode,
                Description = r.Description,
                Stock = r.Stock
            });

            return Result.Success(products);
        }
    }
}
