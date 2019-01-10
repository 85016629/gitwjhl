using fx.Domain.core;
using fx.Domain.CustomerContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Infra.Data.SqlServer
{
    public class CustomerMapper : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasBaseType(typeof(BaseUser));
        }
    }
}
