using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Auth.Model;

namespace StoreManagement.Application.Auth.Command
{
    public class AuthCommand(string username, string password, int companyId) : IRequest<Result<UserToken>>
    {
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
        public int CompanyId { get; set; } = companyId;

        public static AuthCommand CreateCommand(string username, string password, int companyId) =>
            new(username, password, companyId);
    }
}