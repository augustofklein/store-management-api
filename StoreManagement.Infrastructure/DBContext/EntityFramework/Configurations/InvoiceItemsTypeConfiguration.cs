using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class InvoiceItemsTypeConfiguration : IEntityTypeConfiguration<InvoiceItemEntity>
    {
        public void Configure(EntityTypeBuilder<InvoiceItemEntity> builder)
        {
            builder.ToTable("invoice_items");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.InvoiceId)
                .HasColumnName("invoice_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(i => i.ProductId)
                .HasColumnName("product_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(i => i.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(i => i.Quantity)
                .HasColumnName("quantity")
                .HasColumnType("integer")
                .IsRequired();

            builder.HasOne(c => c.Invoice)
               .WithMany(c => c.InvoiceItems)
               .HasForeignKey(c => c.InvoiceId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Product)
               .WithMany(c => c.InvoiceItems)
               .HasForeignKey(c => c.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
