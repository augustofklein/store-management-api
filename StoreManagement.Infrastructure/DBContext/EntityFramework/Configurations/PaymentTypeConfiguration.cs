using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentTypeEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentTypeEntity> builder)
        {
            builder.ToTable("payment_types");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.HasOne(p => p.Company)
                .WithMany(c => c.PaymentTypes)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.CompanyId)
                .HasDatabaseName("idx_payment_types_company_id");
        }
    }
}
