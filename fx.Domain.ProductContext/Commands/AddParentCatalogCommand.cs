using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.ProductContext
{
    public class AddParentCatalogCommand : ICommand
    {
        public AddParentCatalogCommand()
        {
            this.CommandId = Guid.NewGuid();
        }
        public string CatalogName { get; set; }
        public int ParentId { get { return 0; } }

        public Guid CommandId { get ; set ; }
    }
}
