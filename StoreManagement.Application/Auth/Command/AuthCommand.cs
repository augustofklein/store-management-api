using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Auth.Model;

namespace StoreManagement.Application.Auth.Command
{
    public class AuthCommand(string username, string password) : IRequest<Result<UserToken>>
    {
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;

        public static AuthCommand CreateCommand(string username, string password) =>
            new(username, password);
    }
}