using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class EditProductCommand(int id, string description) : IRequest<Result>
    {
        public int Id { get; set; } = id;
        public string Description { get; set; } = description;

        public static EditProductCommand CreateCommand(int id, string description) =>
            new(id, description);
    }
}
