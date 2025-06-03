using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Auth.Command;
using StoreManagement.Application.Auth.Model;
using StoreManagement.Application.Auth.Service;

namespace StoreManagement.Application.Auth.Handler
{
    public class AuthHandler(IAuthService authService, IJwtService jwtService) : IRequestHandler<AuthCommand, Result<UserToken>>
    {
        public async Task<Result<UserToken>> Handle(AuthCommand command, CancellationToken cancellationToken)
        {
            var validation = await authService.ValidateLogin(command.Username, command.Password, cancellationToken);

            return validation.IsFailure
                ? Result.Failure<UserToken>(validation.Error)
                : Result.Success(jwtService.GenerateToken(command.Username));
        }
    }
}
