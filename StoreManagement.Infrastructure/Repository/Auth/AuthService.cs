using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Auth.Model.Dto;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Infrastructure.Repository.Auth
{
    public class AuthService(AppDbContext dbContext) : IAuthService
    {
        private readonly AppDbContext _dbContext = dbContext;
        
        public async Task<Result> AuthenticateUserForCompanyAsync(string email, string password, int companyId, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive, cancellationToken);

            if (user == null)
                return Result.Failure("Invalid email or password.");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return Result.Failure("Invalid email or password.");

            var hasAccessToCompany = await _dbContext.UserCompanies
                .AnyAsync(uc =>
                    uc.UserId == user.Id &&
                    uc.CompanyId == companyId &&
                    uc.IsActive,
                    cancellationToken);

            if (!hasAccessToCompany)
                return Result.Failure("User does not have access to this company.");

            return Result.Success();
        }

        public async Task<Result> EnsureUserExistsAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive, cancellationToken);

            if (user == null)
                return Result.Failure("Invalid email or password.");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return Result.Failure("Invalid email or password.");

            return Result.Success();
        }

        public async Task<Result<IEnumerable<UserCompaniesDto>>> GetUserCompaniesAsync(string email, string password, CancellationToken cancellationToken)
        {
            var userValidation = await EnsureUserExistsAsync(email, password, cancellationToken);
            if(userValidation.IsFailure)
                return Result.Failure<IEnumerable<UserCompaniesDto>>(userValidation.Error);

            var userCompanies = await _dbContext.UserCompanies
                .Where(uc => uc.User.Email == email && uc.IsActive)
                .Select(uc => new UserCompaniesDto
                {
                    CompanyId = uc.CompanyId,
                    DocumentNumber = uc.Company.DocumentNumber,
                    CompanyName = uc.Company.Name
                })
                .ToListAsync(cancellationToken);

            if (userCompanies.Count == 0)
                return Result.Failure<IEnumerable<UserCompaniesDto>>("No companies found for the user.");

            return Result.Success<IEnumerable<UserCompaniesDto>>(userCompanies);
        }
    }
}
