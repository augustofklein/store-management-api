using CSharpFunctionalExtensions;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Invoice.Command;

namespace StoreManagement.Application.Invoice.Service
{
    public class InvoiceService(IProductRepository productRepository) : IInvoiceService
    {
        public async Task<Result> ValidateInvoiceProductsExists(AddInvoiceCommand command, CancellationToken cancellationToken)
        {
            var result = await productRepository.VerifyArrayProductsExistAsync(command.CompanyId, [.. command.InvoiceItems.Select(x => x.ProductId)], cancellationToken);
            if(result.IsFailure)
                return Result.Failure(result.Error);

            return Result.Success();
        }
    }
}
