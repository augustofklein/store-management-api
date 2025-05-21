using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class RemoveProductCommand(int id) : IRequest<Result>
    {
        public int Id { get; private set; } = id;

        public static RemoveProductCommand CreateCommand(int id) =>
            new(id);
    }
}
