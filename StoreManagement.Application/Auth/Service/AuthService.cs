using CSharpFunctionalExtensions;

namespace StoreManagement.Application.Auth.Service
{
    public class AuthService : IAuthService
    {
        //private readonly AppDbContext _dbContext;
        public async Task<Result> ValidateLogin(string username, string password, CancellationToken cancellationToken)
        {
            return Result.Success();
        }
    }
}
