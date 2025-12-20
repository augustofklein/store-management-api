using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class EditProductCommand(bool status, string description) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public bool Status { get; set; } = status;
        public string Description { get; private set; } = description;
    }
}
