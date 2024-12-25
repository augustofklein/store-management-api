using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Product.Command;
using StoreManagement.Application.Product.Model;
using StoreManagement.Application.Product.Queries;

namespace StoreManagement.Application.Product.Handler
{
    public class ProductHandler(IProductQueries productQueries) : IRequestHandler<GetProductsCommand, Result<IEnumerable<ProductDto>>>
    {
        private readonly IProductQueries _productQueries = productQueries;
        public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsCommand command, CancellationToken cancellationToken)
        {
            return await _productQueries.GetProducts(cancellationToken);
        }
    }
}
