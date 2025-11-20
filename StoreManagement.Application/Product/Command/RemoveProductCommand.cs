using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class RemoveProductCommand(int companyId, int id) : IRequest<Result>
    {
        public int CompanyId { get; set; } = companyId;
        public int Id { get; private set; } = id;

        public static RemoveProductCommand CreateCommand(int companyId, int id) =>
            new(companyId, id);
    }
}
