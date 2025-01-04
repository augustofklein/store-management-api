using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Auth.Command;
using StoreManagement.Application.Auth.Service;

namespace StoreManagement.Application.Auth.Handler
{
    public class AuthHandler(IAuthService authService, IJwtService jwtService) : IRequestHandler<AuthCommand, Result<string>>
    {
        private readonly IAuthService _authService = authService;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<Result<string>> Handle(AuthCommand command, CancellationToken cancellationToken)
        {
            var validation = await _authService.ValidateLogin(command.Username, command.Password, cancellationToken);

            if(validation.IsFailure)
                return Result.Failure<string>(validation.Error);

            string token = _jwtService.GenerateToken(command.Username);

            return Result.Success(token);
        }
    }
}
