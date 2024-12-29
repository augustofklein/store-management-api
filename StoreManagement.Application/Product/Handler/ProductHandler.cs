using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Product.Command;
using StoreManagement.Application.Product.Model;
using StoreManagement.Application.Product.Queries;
using StoreManagement.Infrastructure.Repository.Product;

namespace StoreManagement.Application.Product.Handler
{
    public class ProductHandler(IProductQueries productQueries,
                                IProductRepository productRepository) : IRequestHandler<GetProductsCommand, Result<IEnumerable<ProductDto>>>,
                                                                        IRequestHandler<AddProductCommand, Result>,
                                                                        IRequestHandler<RemoveProductCommand, Result>,
                                                                        IRequestHandler<EditProductCommand, Result>
    {
        private readonly IProductQueries _productQueries = productQueries;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsCommand command, CancellationToken cancellationToken)
        {
            return await _productQueries.GetProducts(cancellationToken);
        }

        public async Task<Result> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _productRepository.AddProduct(command.Id, command.Description, command.Stock, cancellationToken);

            if(result.IsFailure)
                return Result.Failure(result.Error);

            return Result.Success();
        }

        public async Task<Result> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _productRepository.RemoveProduct(command.Id, cancellationToken);

            if(result.IsFailure)
                return Result.Failure(result.Error);

            return Result.Success();
        }

        public async Task<Result> Handle(EditProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _productRepository.EditProduct(command.Id, command.Description, cancellationToken);

            if(result.IsFailure)
                return Result.Failure(result.Error);

            return Result.Success();
        }
    }
}
