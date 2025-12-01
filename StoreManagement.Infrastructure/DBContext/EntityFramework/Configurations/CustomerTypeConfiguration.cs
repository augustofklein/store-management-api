using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class CustomerTypeConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.ToTable("customer")
                .HasKey(c => new { c.Id });

            builder.HasOne(x => x.Company)
               .WithMany(x => x.Customers)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Identification)
                .HasColumnName("identification")
                .HasColumnType("varchar(18)");

            builder.Property(p => p.Address)
                .HasColumnName("address")
                .HasColumnType("varchar(100)");
        }
    }
}
