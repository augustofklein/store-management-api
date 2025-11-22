using CSharpFunctionalExtensions;

namespace StoreManagement.Application.Contracts.Persistence
{
    public interface IAuthService
    {
        Task<Result> ValidateLogin(string username, string password, CancellationToken cancellationToken);
    }
}
