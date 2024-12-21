using CSharpFunctionalExtensions;

namespace StoreManagement.Application.Auth.Service
{
    public interface IAuthService
    {
        Task<Result> ValidateLogin(string username, string password, CancellationToken cancellationToken);
    }
}
