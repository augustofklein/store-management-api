using System;

namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserEntity(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public UserEntity()
        {
            Id = default;
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
