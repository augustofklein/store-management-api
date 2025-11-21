using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Product.Command;
using StoreManagement.Application.Product.Service;
using StoreManagement.Infrastructure.Repository.Product;

namespace StoreManagement.Application.Product.Handler
{
    public class ProductHandler(IProductRepository productRepository, IProductService productService) :
        IRequestHandler<AddProductCommand, Result>,
        IRequestHandler<RemoveProductCommand, Result>,
        IRequestHandler<EditProductCommand, Result>
    {
        public async Task<Result> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var validation = await productService.ValidadeAddProductAsync(command.CompanyId, command.SkuId, cancellationToken);
            if(validation.IsFailure)
                return Result.Failure(validation.Error);

            var result = await productRepository.AddProduct(command.CompanyId, command.SkuId, command.Status, command.Barcode, command.Description, command.Stock, cancellationToken);
            if(result.IsFailure)
                return Result.Failure(result.Error);

            return Result.Success();
        }

        public async Task<Result> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
        {
            var validation = await productService.ValidateEditDeleteProduct(command.CompanyId, command.Id, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure(validation.Error);

            var result = await productRepository.RemoveProduct(command.CompanyId, command.Id, cancellationToken);
            if(result.IsFailure)
                return Result.Failure(result.Error);

            return Result.Success();
        }

        public async Task<Result> Handle(EditProductCommand command, CancellationToken cancellationToken)
        {
            var validation = await productService.ValidateEditDeleteProduct(command.CompanyId, command.Id, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure(validation.Error);

            var result = await productRepository.EditProduct(command.CompanyId, command.Id, command.Status, command.Description, cancellationToken);
            if(result.IsFailure)
                return Result.Failure(result.Error);

            return Result.Success();
        }
    }
}
