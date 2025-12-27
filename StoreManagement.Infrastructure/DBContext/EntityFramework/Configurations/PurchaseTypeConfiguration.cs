using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class PurchaseTypeConfiguration : IEntityTypeConfiguration<PurchaseEntity>
    {
        public void Configure(EntityTypeBuilder<PurchaseEntity> builder)
        {
            builder.ToTable("purchases");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("integer");

            builder.Property(i => i.SupplierId)
                .HasColumnName("supplier_id")
                .HasColumnType("integer");

            builder.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DocumentKey)
                .HasColumnName("document_key")
                .HasColumnType("varchar(44)")
                .IsRequired();

            builder.Property(i => i.PurchaseDate)
                .HasColumnName("purchase_date")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(i => i.TotalAmount)
                .HasColumnName("total_amount")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(c => c.Company)
               .WithMany(c => c.Purchases)
               .HasForeignKey(c => c.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Supplier)
               .WithMany(c => c.Purchases)
               .HasForeignKey(c => c.SupplierId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
