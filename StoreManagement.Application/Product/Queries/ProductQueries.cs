using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Product.Model;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Application.Product.Queries
{
    public class ProductQueries(AppDbContext dbContext) : IProductQueries
    {
        private readonly AppDbContext _dbContext = dbContext;
        public async Task<Result<IEnumerable<ProductDto>>> GetProducts(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.Products.AsNoTracking().ToListAsync(cancellationToken);

                var products = result.Select(r => new ProductDto
                {
                    Id = r.Id,
                    Barcode = r.Barcode,
                    Description = r.Description,
                    Stock = r.Stock
                });

                return Result.Success(products);
            }
            catch(Exception e)
            {
                return Result.Failure<IEnumerable<ProductDto>>($"An error occurred: {e.Message}");
            }
        }
    }
}
