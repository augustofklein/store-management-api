using System;

namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class UserEntity
    {
        public int Id { get; private set; }
        public int CompanyId { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
    }
}
