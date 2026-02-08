using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class EditProductCommand(bool status, string description, decimal price) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public bool Status { get; set; } = status;
        public string Description { get; private set; } = description;
        public decimal Price { get; set; } = price;
    }
}
