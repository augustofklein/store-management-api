using CSharpFunctionalExtensions;
using StoreManagement.Application.Auth.Model.Dto;

namespace StoreManagement.Application.Contracts.Persistence
{
    public interface IAuthService
    {
        Task<Result> AuthenticateUserForCompanyAsync(string username, string password, int companyId, CancellationToken cancellationToken);
        Task<Result> EnsureUserExistsAsync(string email, string password, CancellationToken cancellationToken);
        Task<Result<IEnumerable<UserCompaniesDto>>> GetUserCompaniesAsync(string email, string password, CancellationToken cancellationToken);
    }
}
