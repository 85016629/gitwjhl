using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.ProductContext
{
    public class AddSubCatalogCommand : BaseCommand
    {
        public Guid CommandId { get ; set ; }
        public int ParentId { get; set; }
        public string CatalogName { get; set; }

        public AddSubCatalogCommand(string catalogName, int parentId)
        {
            this.CommandId = Guid.NewGuid();
            this.ParentId = parentId;
            this.CatalogName = catalogName;
        }
    }
}
