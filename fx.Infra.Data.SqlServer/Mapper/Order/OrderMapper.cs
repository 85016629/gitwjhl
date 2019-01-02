namespace fx.Infra.Data.SqlServer
{
    using fx.Domain.OrderContext;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    public class OrderMapper : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
        }
    }
}
