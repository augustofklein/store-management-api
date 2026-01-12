namespace StoreManagement.WebApi.InputModel.User
{
    public class UserCredencials(string username, string password)
    {
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
    }
}
