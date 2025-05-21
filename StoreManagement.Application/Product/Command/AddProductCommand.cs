using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class AddProductCommand(int id, string barcode, string description, int stock) : IRequest<Result>
    {
        public int Id { get; private set; } = id;
        public string Barcode { get; private set; } = barcode;
        public string Description { get; private set; } = description;
        public int Stock { get; private set; } = stock;
    }
}
