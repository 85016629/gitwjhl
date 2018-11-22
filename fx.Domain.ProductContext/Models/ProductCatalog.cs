using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.ProductContext
{
    public class ProductCatalog : AggregateRoot<int>
    {
        public ProductCatalog() { }
        public string CatalogName { get; set; }
        public int ParentId { get; set; }
        public void AddChildCatalog(string catalogName, IMemoryBus bus)
        {
            var ev = new SubCatalogAdded(catalogName)
            {
                ParentId = this.Id
            };

            bus.RaiseEvent(ev);
        }
    }
}
