using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("products").HasKey(product => new { product.Id });

            builder.Property(user => user.Id).HasColumnName("id").HasColumnType("integer");
            builder.Property(user => user.Description).HasColumnName("description").HasColumnType("varchar(50)");
            builder.Property(user => user.Stock).HasColumnName("stock").HasColumnType("integer");
        }
    }
}
