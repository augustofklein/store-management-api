using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class UserRoleTypeConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable("user_roles");

            builder.HasKey(ur => ur.Id);
            
            builder.Property(ur => ur.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            
            builder.Property(ur => ur.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(50)")
                .IsRequired();
        }
    }
}
