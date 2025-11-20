using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Infrastructure.DBContext;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.Repository.Product
{
    public class ProductRepository(AppDbContext dbContext) : IProductRepository
    {
        public async Task<Result> AddProduct(int companyId, string skuId, bool status, string barcode, string description, int stock, CancellationToken cancellationToken)
        {
            var product = new ProductEntity
            {
                CompanyId = companyId,
                Id = 0,
                SkuId = skuId.Trim(),
                Status = status,
                Barcode = barcode,
                Description = description,
                Stock = stock
            };

            await dbContext.Products.AddAsync(product, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> RemoveProduct(int companyId, int id, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                .FirstOrDefaultAsync(
                    p => p.CompanyId == companyId && p.Id == id,
                    cancellationToken
                );
            
            if (product == null)
            {
                return Result.Failure($"Product with ID {id} not found.");
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> EditProduct(int companyId, int id, bool status, string description, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                .FirstOrDefaultAsync(
                    p => p.CompanyId == companyId && p.Id == id,
                    cancellationToken
                );

            if (product == null)
            {
                return Result.Failure($"Product with ID {id} not found.");
            }

            product.Description = description;
            product.Status = status;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
