using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class ContactTypeConfiguration : IEntityTypeConfiguration<ContactTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ContactTypeEntity> builder)
        {
            builder.ToTable("contact_type")
                .HasKey(c => new { c.Id });

            builder.Property(c => c.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(50)");
        }
    }
}
