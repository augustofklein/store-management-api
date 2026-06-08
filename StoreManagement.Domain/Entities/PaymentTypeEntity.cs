using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Domain.Entities
{
    public class PaymentTypeEntity
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual CompanyEntity Company { get; set; } = null!;
    }
}
