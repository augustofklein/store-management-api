using System.ComponentModel.DataAnnotations;

namespace StoreManagement.WebApi.Model
{
    public class User(string username, string password)
    {
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
    }
}
