using CSharpFunctionalExtensions;
using StoreManagement.Infrastructure.DBContext;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.Repository.Product
{
    public class ProductRepository(AppDbContext dbContext) : IProductRepository
    {
        public async Task<Result> AddProduct(int id, string barcode, string description, int stock, CancellationToken cancellationToken)
        {
            var product = new ProductEntity
            {
                Id = id,
                Barcode = barcode,
                Description = description,
                Stock = stock
            };

            await dbContext.Products.AddAsync(product, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> RemoveProduct(int id, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync([id], cancellationToken);
            
            if (product == null)
            {
                return Result.Failure($"Product with ID {id} not found.");
            }

            dbContext.Products.Remove(product);

            return Result.Success();
        }

        public async Task<Result> EditProduct(int id, string description, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync([id], cancellationToken);
            
            if (product == null)
            {
                return Result.Failure($"Product with ID {id} not found.");
            }

            product.Description = description;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
