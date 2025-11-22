using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Application.Auth.Service
{
    public class AuthService(AppDbContext dbContext) : IAuthService
    {
        private readonly AppDbContext _dbContext = dbContext;
        
        public async Task<Result> ValidateLogin(string email, string password, int companyId, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Email == email, cancellationToken);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return Result.Failure("Invalid email or password.");

            return Result.Success();
        }
    }
}
