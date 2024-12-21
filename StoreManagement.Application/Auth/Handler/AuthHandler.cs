using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Auth.Command;
using StoreManagement.Application.Auth.Service;

namespace StoreManagement.Application.Auth.Handler
{
    public class AuthHandler(IAuthService authService) : IRequestHandler<AuthCommand, Result>
    {
        private readonly IAuthService _authService = authService;
        public async Task<Result> Handle(AuthCommand command, CancellationToken cancellationToken)
        {
            var validation = await _authService.ValidateLogin(command.Username, command.Password, cancellationToken);

            if(validation.IsFailure)
                return Result.Failure(validation.Error);

            return Result.Success();
        }
    }
}
