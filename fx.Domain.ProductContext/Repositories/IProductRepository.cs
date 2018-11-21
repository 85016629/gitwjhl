using System;
using System.Collections.Generic;
using System.Text;
using fx.Domain.core;

namespace fx.Domain.ProductContext
{
    public interface IProductRepository : IRepository<Product,Guid>
    {

    }
}
