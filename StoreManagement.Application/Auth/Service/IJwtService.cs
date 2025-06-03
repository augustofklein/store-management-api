using StoreManagement.Application.Auth.Model;

namespace StoreManagement.Application.Auth.Service
{
    public interface IJwtService
    {
        UserToken GenerateToken(string username);
    }
}
