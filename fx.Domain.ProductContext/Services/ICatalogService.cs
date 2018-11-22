using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.ProductContext
{
    public interface ICatalogService
    {
        IList<ProductCatalog> GetAllProductCatalogs();
        void AddCatalog(string catalogName);
    }
}
