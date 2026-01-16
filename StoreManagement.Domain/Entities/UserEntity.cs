using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public ICollection<UserCompanyEntity> UserCompanies { get; set; } = [];
    }
}
