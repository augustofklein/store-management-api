using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Customer.Command
{
    public record DeleteCustomerCommand(int CompanyId, int Id) : IRequest<Result>;
}
