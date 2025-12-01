using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class CustomerContactTypeConfiguration : IEntityTypeConfiguration<CustomerContactEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerContactEntity> builder)
        {
            builder.ToTable("customer_contact")
                .HasKey(c => new { c.Id });

            builder.HasOne(x => x.Customer)
               .WithMany(x => x.CustomerContacts)
               .HasForeignKey(x => x.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Id)
                .HasColumnName("contact")
                .HasColumnType("varchar(50)");
        }
    }
}
