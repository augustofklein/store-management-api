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
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive, cancellationToken);

            if (user == null)
                return Result.Failure("Invalid email or password.");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return Result.Failure("Invalid email or password.");

            var hasAccessToCompany = await _dbContext.UserCompanies
                .AsNoTracking()
                .AnyAsync(uc =>
                    uc.UserId == user.Id &&
                    uc.CompanyId == companyId &&
                    uc.IsActive,
                    cancellationToken);

            if (!hasAccessToCompany)
                return Result.Failure("User does not have access to this company.");

            return Result.Success();
        }
    }
}
