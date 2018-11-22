using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.ProductContext
{
    public class SubCatalogAdded : DomainEvent
    {
        private string _catalogName;

        public SubCatalogAdded(string catalogName)
        {
            _catalogName = catalogName;
        }
        public int ParentId { get; set; }

        public string CatalogName { get => _catalogName; set => _catalogName = value; }

    }
}
