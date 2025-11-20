using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class EditProductCommand(int companyId, int id, bool status, string description) : IRequest<Result>
    {
        public int CompanyId { get; set; } = companyId;
        public int Id { get; private set; } = id;
        public bool Status { get; set; } = status;
        public string Description { get; private set; } = description;

        public static EditProductCommand CreateCommand(int companyId, int id, bool status, string description) =>
            new(companyId, id, status, description);
    }
}
