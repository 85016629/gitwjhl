﻿using fx.Domain.ProductContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Infra.Data.SqlServer
{
    public class CatalogRepository : BaseRepository<ProductCatalog, int>, ICatalogRepository
    {
    }
}
