using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class ProductMovementTypeConfiguration : IEntityTypeConfiguration<ProductMovementEntity>
    {
        public void Configure(EntityTypeBuilder<ProductMovementEntity> builder)
        {
            builder.ToTable("product_movements");
            
            builder.HasKey(pm => pm.Id);
            
            builder.Property(pm => pm.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(pm => pm.ProductId)
                .HasColumnName("product_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(pm => pm.MovementType)
                .HasColumnName("movement_type")
                .HasColumnType("smallint")
                .IsRequired();
            
            builder.Property(pm => pm.Quantity)
                .HasColumnName("quantity")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(pp => pp.Price)
                .HasColumnName("price")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(pm => pm.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("now()")
                .IsRequired();

            builder.HasOne(p => p.Product)
               .WithMany(p => p.ProductMovements)
               .HasForeignKey(p => p.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(pm => new { pm.ProductId, pm.CreatedAt })
                .HasDatabaseName("idx_product_movements_product_date");
        }
    }
}
