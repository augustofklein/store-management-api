namespace StoreManagement.Application.Auth.Model;

public class UserToken(string accessToken)
{
    public string AccessToken { get; set; } = accessToken;
}