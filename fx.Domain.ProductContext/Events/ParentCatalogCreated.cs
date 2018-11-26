using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.ProductContext
{
    public class ParentCatalogCreated : DomainEvent
    {
        public ParentCatalogCreated(string catalogName)
        {
            this.CatalogName = catalogName;
        }
        public string CatalogName { get; set; }
        public int ParentId { get { return 0; } }
    }
}
