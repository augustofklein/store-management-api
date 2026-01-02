using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Supplier.Command;
using StoreManagement.Application.Supplier.Model;
using StoreManagement.Application.Supplier.Service;

namespace StoreManagement.Application.Supplier.Handler
{
    public class SupplierHandler(ISupplierRepository supplierRepository, ISupplierService supplierService, IMapper mapper) :
        IRequestHandler<DeleteSupplierCommand, Result>,
        IRequestHandler<AddSupplierCommand, Result>
    {
        public async Task<Result> Handle(DeleteSupplierCommand command, CancellationToken cancellationToken)
        {
            var validation = await supplierService.ValidadeDeleteSupplierAsync(command.CompanyId, command.Id, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure(validation.Error);

            var result = await supplierRepository.DeleteSupplierAsync(command.CompanyId, command.Id, cancellationToken);
            if (result.IsFailure)
                return Result.Failure(result.Error);

            return Result.Success();
        }

        public async Task<Result> Handle(AddSupplierCommand command, CancellationToken cancellationToken)
        {
            var validation = await supplierService.ValidateAddSupplierAsync(command.CompanyId, command.Identification, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure(validation.Error);

            return Result.Success(await supplierRepository.AddSupplierAsync(command.CompanyId, mapper.Map<AddSupplierDto>(command), cancellationToken));
        }
    }
}
