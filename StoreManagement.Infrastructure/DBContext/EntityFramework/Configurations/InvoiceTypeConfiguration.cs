using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class InvoiceTypeConfiguration : IEntityTypeConfiguration<InvoiceEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<InvoiceEntity> builder)
        {
            builder.ToTable("invoices");
            
            builder.HasKey(i => i.Id);
            
            builder.Property(i => i.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("integer");

            builder.Property(i => i.CustomerId)
                .HasColumnName("customer_id")
                .HasColumnType("integer");

            builder.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            
            builder.Property(i => i.InvoiceDate)
                .HasColumnName("invoice_date")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(i => i.TotalAmount)
                .HasColumnName("total_amount")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(c => c.Company)
               .WithMany(c => c.Invoices)
               .HasForeignKey(c => c.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Customer)
               .WithMany(c => c.Invoices)
               .HasForeignKey(c => c.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
