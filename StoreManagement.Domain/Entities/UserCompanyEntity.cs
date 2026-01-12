using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Domain.Entities
{
    public class UserCompanyEntity
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual CompanyEntity Company { get; set; } = null!;
        public virtual UserEntity User { get; set; } = null!;
        public virtual UserRoleEntity Role { get; set; } = null!;
    }
}
