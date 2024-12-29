using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class EditProductCommand(int id, string description, int stock) : IRequest<Result>
    {
        public int Id { get; set; } = id;
        public string Description { get; set; } = description;
        public int Stock { get; set; } = stock;

        public static EditProductCommand CreateCommand(int id, string description, int stock) =>
            new(id, description, stock);
    }
}
