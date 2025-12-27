using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Invoice.Command;
using StoreManagement.Application.Invoice.Model;
using StoreManagement.Application.Invoice.Service;

namespace StoreManagement.Application.Invoice.Handler
{
    public class InvoiceHandler(IInvoiceRepository invoiceRepository, IInvoiceService invoiceService, IMapper mapper) : IRequestHandler<AddInvoiceCommand, Result>
    {
        public async Task<Result> Handle(AddInvoiceCommand command, CancellationToken cancellationToken)
        {
            var validation = await invoiceService.ValidateInvoiceProductsExists(command, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure(validation.Error);

            return Result.Success(await invoiceRepository.AddInvoiceAsync(mapper.Map<AddInvoiceDto>(command), cancellationToken));
        }
    }
}
