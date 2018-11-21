using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.ProductContext
{
    public class ProductCatalog : AggregateRoot<Guid>
    {
        public int IndexId { get; set; }
        public string CatalogName { get; set; }
        public ProductCatalog ParentCatalog { get; set; }
        public List<ProductCatalog> ChildrenCatalog { get; set; }
        public void AddChildCatalog(ProductCatalog item)
        {
            ChildrenCatalog.Add(item);
        }
    }
}
