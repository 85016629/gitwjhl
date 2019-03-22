using fx.Domain.CustomerContext.QueryStack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.CustomerContext.QueryStack.DtoMappers
{
    public class CustomerDtoDataMapper : IEntityTypeConfiguration<CustomerDto>
    {
        public void Configure(EntityTypeBuilder<CustomerDto> builder)
        {

            builder.Property(c => c.LoginId)
                .HasColumnName("LoginId");

            builder.Property(c => c.MobilePhone)
               .HasColumnName("MobilePhone");
        }
    }
}
