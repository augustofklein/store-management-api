using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class CompanyEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public CompanyEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
