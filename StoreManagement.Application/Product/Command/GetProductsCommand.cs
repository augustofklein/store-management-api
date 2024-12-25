using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Product.Model;

namespace StoreManagement.Application.Product.Command
{
    public class GetProductsCommand : IRequest<Result<IEnumerable<ProductDto>>>
    {
        public static GetProductsCommand CreateCommand() =>
            new();
    }
}
