using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Supplier.Command
{
    public class AddSupplierCommand(string identification, string name) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public string Identification { get; set; } = identification;
        public string Name { get; set; } = name;
    }
}
