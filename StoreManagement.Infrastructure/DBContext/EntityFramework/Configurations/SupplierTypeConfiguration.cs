using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class SupplierTypeConfiguration : IEntityTypeConfiguration<SupplierEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SupplierEntity> builder)
        {
            builder.ToTable("suppliers");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(s => s.DocumentNumber)
                .HasColumnName("document_number")
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder.Property(s => s.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
