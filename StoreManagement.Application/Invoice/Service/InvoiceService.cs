using AutoMapper;
using CSharpFunctionalExtensions;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Invoice.Command;
using StoreManagement.Application.Product.Model;

namespace StoreManagement.Application.Invoice.Service
{
    public class InvoiceService(IProductRepository productRepository, ICustomerRepository customerRepository, IMapper mapper) : IInvoiceService
    {
        public async Task<Result> ValidateAddInvoiceAsync(AddInvoiceCommand command, CancellationToken cancellationToken)
        {
            if(command.InvoiceItems.Count == 0)
                return Result.Failure("Invoice must contain at least one item.");

            if(!await customerRepository.VerifyCustomerByIdExistAsync(command.CompanyId, command.CustomerId, cancellationToken))
                return Result.Failure("Customer does not exist.");

            var validateAllProductsExist = await productRepository.VerifyArrayProductsExistAsync(command.CompanyId, [.. command.InvoiceItems.Select(x => x.ProductId)], cancellationToken);
            if(validateAllProductsExist.IsFailure)
                return Result.Failure(validateAllProductsExist.Error);

            var validateStockAvailability = await productRepository.ValidateInvoiceProductsStockAsync(command.CompanyId, mapper.Map<List<ProductStockDto>>(command.InvoiceItems), cancellationToken);
            if(validateStockAvailability.IsFailure)
                return Result.Failure(validateStockAvailability.Error);

            return Result.Success();
        }
    }
}
