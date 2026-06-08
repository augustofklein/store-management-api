using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class IncoicePaymentsConfiguration : IEntityTypeConfiguration<IncoicePaymentsEntity>
    {
        public void Configure(EntityTypeBuilder<IncoicePaymentsEntity> builder)
        {
            builder.ToTable("invoice_payments");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.InvoiceId)
                .HasColumnName("invoice_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(i => i.PaymentTypeId)
                .HasColumnName("payment_type_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(i => i.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(12,2)")
                .IsRequired();

            builder.Property(i => i.PaymentDate)
                .HasColumnName("payment_date")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.HasOne(i => i.Invoice)
                .WithMany(i => i.InvoicePayments)
                .HasForeignKey(i => i.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.PaymentType)
                .WithMany()
                .HasForeignKey(i => i.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(i => i.CompanyId)
                .HasDatabaseName("idx_invoice_payments_company_id");

            builder.HasIndex(i => i.InvoiceId)
                .HasDatabaseName("idx_invoice_payments_invoice_id");

            builder.HasIndex(i => i.PaymentTypeId)
                .HasDatabaseName("idx_invoice_payments_payment_type_id");

            builder.HasIndex(i => i.PaymentDate)
                .HasDatabaseName("idx_invoice_payments_date");
        }
    }
}
