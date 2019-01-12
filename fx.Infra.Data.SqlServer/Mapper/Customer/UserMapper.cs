using fx.Domain.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace fx.Infra.Data.SqlSever
{
    public class UserMapper : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(t => t.UUId);

            builder.Property(t => t.LoginId).HasColumnName("LoginId");
            //builder.Property(t => t.UUId).HasColumnName("UUId");            

            builder.Property(t => t.MobilePhone)
                .HasColumnName("MobilePhone");

            builder.Property(t => t.Password)
                .HasColumnName("Password")
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnType("Varchar");

            builder.Property(t => t.RegisterTime)
                .HasColumnName("RegisterTime");

            builder.Property(t => t.Username)
                .HasColumnName("Username")
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnType("Nvarchar");

            builder.Property(t => t.Email)
                .HasColumnName("Email");            
                
            builder.Ignore(c => c.UserRoles);
        }
    }
}
