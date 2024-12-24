namespace StoreManagement.Application.Auth.Service
{
    public interface IJwtService
    {
        string GenerateToken(string username);
    }
}
