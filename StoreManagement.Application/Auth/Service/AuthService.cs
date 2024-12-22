using CSharpFunctionalExtensions;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Application.Auth.Service
{
    public class AuthService(AppDbContext dbContext) : IAuthService
    {
        private readonly AppDbContext _dbContext = dbContext;
        public async Task<Result> ValidateLogin(string username, string password, CancellationToken cancellationToken)
        {
            return Result.Success();
        }
    }
}
