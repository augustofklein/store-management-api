using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class CustomerTypeConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.ToTable("customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("integer");

            builder.Property(c => c.Identification)
                .HasColumnName("identification")
                .HasColumnType("varchar(11)");
            
            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Address)
                .HasColumnName("address")
                .HasColumnType("varchar(100)");

            builder.HasOne(c => c.Company)
               .WithMany(c => c.Customers)
               .HasForeignKey(c => c.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CustomerContacts)
              .WithOne(x => x.Customer)
              .HasForeignKey(x => x.CustomerId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
