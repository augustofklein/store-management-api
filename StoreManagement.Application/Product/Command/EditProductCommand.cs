using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class EditProductCommand(int id, string description) : IRequest<Result>
    {
        public int Id { get; private set; } = id;
        public string Description { get; private set; } = description;

        public static EditProductCommand CreateCommand(int id, string description) =>
            new(id, description);
    }
}
