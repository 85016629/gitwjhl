using fx.Domain.ProductContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Application.Product
{
    public interface ICatalogService
    {
        IEnumerable<ProductCatalog> GetAllProductCatalogs();
        void AddCatalog(string catalogName);
    }
}
