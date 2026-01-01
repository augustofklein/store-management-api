using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Supplier.Command
{
    public class DeleteSupplierCommand(int companyId, int id) : IRequest<Result>
    {
        public int CompanyId { get; set; } = companyId;
        public int Id { get; private set; } = id;

        public static DeleteSupplierCommand CreateCommand(int companyId, int id) =>
            new(companyId, id);
    }
}
