using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class AddProductCommand(int id, string description, int stock) : IRequest<Result>
    {
        public int Id { get; set; } = id;
        public string Description { get; set; } = description;
        public int Stock { get; set; } = stock;

        public static AddProductCommand CreateCommand(int id, string description, int stock) =>
            new(id, description, stock);
    }
}
