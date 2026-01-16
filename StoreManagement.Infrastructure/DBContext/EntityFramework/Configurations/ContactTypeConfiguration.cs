using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class ContactTypeConfiguration : IEntityTypeConfiguration<ContactTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ContactTypeEntity> builder)
        {
            builder.ToTable("contact_type");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(50)")
                .IsRequired();
        }
    }
}
