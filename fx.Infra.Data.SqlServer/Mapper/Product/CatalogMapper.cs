using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using fx.Domain.ProductContext;

namespace fx.Infra.Data.SqlServer.Mapper.Product
{
    public class CatalogMapper : IEntityTypeConfiguration<ProductCatalog>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductCatalog> builder)
        {
            builder.ToTable("ProductCatalog");

            builder.Property(p => p.UUId).HasColumnName("Id");
            builder.HasKey(p => p.UUId);
        }
    }
}
