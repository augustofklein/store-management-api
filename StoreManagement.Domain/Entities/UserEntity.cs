namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class UserEntity
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public virtual CompanyEntity Company { get; set; } = null!;
    }
}
