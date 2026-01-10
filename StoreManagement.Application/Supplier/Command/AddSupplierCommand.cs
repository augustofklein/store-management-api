using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Supplier.Command
{
    public class AddSupplierCommand(string documentNumber, string name) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public string DocumentNumber { get; set; } = documentNumber;
        public string Name { get; set; } = name;
    }
}
