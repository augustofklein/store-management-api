using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users").HasKey(user => new { user.Id });

            builder.Property(user => user.Id).HasColumnName("id").HasColumnType("serial");
            builder.Property(user => user.Username).HasColumnName("username").HasColumnType("varchar(50)");
            builder.Property(user => user.Password).HasColumnName("password").HasColumnType("text");
        }
    }
}
