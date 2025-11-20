namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class UserEntity
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
