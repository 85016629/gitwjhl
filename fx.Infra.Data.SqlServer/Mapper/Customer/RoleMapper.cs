using fx.Domain.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.ef.SqlSever
{
    public class RoleMapper : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.Property(t => t.RoleName)
                .HasColumnName("RoleName")
                .IsRequired();

            builder.Property(t => t.RoleDesc).HasColumnName("RoleDesc")
                .HasColumnType("NVarchar")
                .IsRequired()
                .HasMaxLength(40);

        }
    }
}
