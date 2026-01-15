using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Invoice.Command;
using StoreManagement.Application.Invoice.Model;
using StoreManagement.Application.Invoice.Service;
using StoreManagement.Application.Product.Model;
using StoreManagement.Domain.Enums;

namespace StoreManagement.Application.Invoice.Handler
{
    public class InvoiceHandler(IInvoiceRepository invoiceRepository, IProductRepository productRepository, IInvoiceService invoiceService, IMapper mapper, IEFTransactionManager eFTransactionManager) : IRequestHandler<AddInvoiceCommand, Result>
    {
        public async Task<Result> Handle(AddInvoiceCommand command, CancellationToken cancellationToken)
        {
            var validation = await invoiceService.ValidateInvoiceProductsExists(command, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure(validation.Error);

            await eFTransactionManager.BeginAsync(cancellationToken);

            try
            {
                await invoiceRepository.AddInvoiceAsync(mapper.Map<AddInvoiceDto>(command), cancellationToken);

                var updateStockResult = await productRepository.UpdateProductStockArrayAsync(command.CompanyId, ProductMovementEnum.INVOICE, mapper.Map<List<UpdateProductStockDto>>(command.InvoiceItems), cancellationToken);
                if(updateStockResult.IsFailure)
                    return Result.Failure(updateStockResult.Error);

                await productRepository.AddProductMovementArrayAsync(ProductMovementEnum.INVOICE, command.InvoiceDate, mapper.Map<List<AddProductMovementDto>>(command.InvoiceItems), cancellationToken);

                await eFTransactionManager.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception)
            {
                await eFTransactionManager.RollbackAsync(cancellationToken);
                throw;
            }

        }
    }
}
