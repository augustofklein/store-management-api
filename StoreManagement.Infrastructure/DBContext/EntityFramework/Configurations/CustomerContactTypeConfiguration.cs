using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class CustomerContactTypeConfiguration : IEntityTypeConfiguration<CustomerContactEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerContactEntity> builder)
        {
            builder.ToTable("customer_contact");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.CustomerId)
                .HasColumnName("customer_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(c => c.ContactTypeId)
                .HasColumnName("contact_type_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(c => c.Contact)
                .HasColumnName("contact")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.HasOne(c => c.Customer)
               .WithMany(c => c.CustomerContacts)
               .HasForeignKey(c => c.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.ContactType)
               .WithMany(c => c.CustomerContacts)
               .HasForeignKey(c => c.ContactTypeId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
