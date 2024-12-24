using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Application.Auth.Service
{
    public class AuthService(AppDbContext dbContext) : IAuthService
    {
        private readonly AppDbContext _dbContext = dbContext;
        
        public async Task<Result> ValidateLogin(string username, string password, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(e => e.Username == username, cancellationToken);

            if (user == null)
            {
                return Result.Failure("Invalid username or password.");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return Result.Failure("Invalid username or password.");
            }

            return Result.Success();
        }
    }
}
