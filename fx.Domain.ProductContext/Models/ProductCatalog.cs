﻿using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace fx.Domain.ProductContext
{
    public class ProductCatalog : AggregateRoot<int>
    {
        public ProductCatalog() { }
        public int ProductCatalogId { get; set; }
        public string CatalogName { get; set; }
        public int ParentId { get; set; }
        public void AddChildCatalog(string catalogName, IMemoryBus bus)
        {
            var ev = new SubCatalogAdded(catalogName)
            {
                ParentId = this.ProductCatalogId
            };

            bus.RaiseEvent(ev);
        }

        public void Create(IMemoryBus bus)
        {
            var ev = new ParentCatalogCreated(this.CatalogName);

            bus.RaiseEvent(ev);
        }
    }
}
