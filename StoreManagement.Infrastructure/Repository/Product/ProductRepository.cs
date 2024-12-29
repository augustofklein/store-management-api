using CSharpFunctionalExtensions;
using StoreManagement.Infrastructure.DBContext;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.Repository.Product
{
    public class ProductRepository(AppDbContext dbContext) : IProductRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Result> AddProduct(int id, string description, int stock, CancellationToken cancellationToken)
        {
            try
            {
                var product = new ProductEntity
                {
                    Id = id,
                    Description = description,
                    Stock = stock
                };

                await _dbContext.Products.AddAsync(product, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure($"An error occurred while adding the product: {ex.Message}");
            }
        }

        public async Task<Result> RemoveProduct(int id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _dbContext.Products.FindAsync([id], cancellationToken);
                
                if (product == null)
                {
                    return Result.Failure($"Product with ID {id} not found.");
                }

                _dbContext.Products.Remove(product);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure($"An error occurred while removing the product: {ex.Message}");
            }
        }

        public async Task<Result> EditProduct(int id, string description, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _dbContext.Products.FindAsync([id], cancellationToken);
                
                if (product == null)
                {
                    return Result.Failure($"Product with ID {id} not found.");
                }

                product.Description = description;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure($"An error occurred while updating the product: {ex.Message}");
            }
        }
    }
}
