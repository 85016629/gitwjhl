using fx.Domain.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Infra.Data.SqlSever
{
    public class UserRoleRelationMapper : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("UserRoleRelations");

            
            builder.HasKey(ur => new { ur.RoleId, ur.UserId });
            builder.Property(r => r.UserId).IsRequired();
            builder.Property(r => r.RoleId).IsRequired();

            //一个用户，对应多个角色
            builder.HasOne(r => r.User)
                .WithMany(p => p.UserRoles)                
                .HasForeignKey(r => r.UserId)
                .IsRequired();
               
            //一个角色，对应多个用户
            builder.HasOne(r => r.Role)
                .WithMany(p=>p.UserRoles)
                .HasForeignKey(r => r.RoleId)
                .IsRequired()
                .HasForeignKey("RoleId");
        }
    }
}
