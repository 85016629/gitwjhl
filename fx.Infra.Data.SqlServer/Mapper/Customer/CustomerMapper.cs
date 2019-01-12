using fx.Domain.core;
using fx.Domain.CustomerContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Infra.Data.SqlServer.Mapper
{
    public class CustomerMapper : IEntityTypeConfiguration<Customer>
    {
        public CustomerMapper()
        {
            
        }
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            
            builder.ToTable("Customers");

            builder.Property(p => p.VipLevel)
                .HasColumnName("VipLevel");

            builder.HasBaseType(typeof(BaseUser))
                .ToTable("Customers");
            


            //Map<Customer>(u=>u.ToTable("BaseUsers"));

        }
    }
}
