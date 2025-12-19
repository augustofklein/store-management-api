using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Customer.Command
{
    public record DeleteCustomerCommand(int companyId, int Id) : IRequest<Result>;
}
