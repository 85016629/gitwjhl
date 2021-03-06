﻿using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace fx.Domain.ProductContext
{
    public class Product : AggregateRoot<Guid>
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品目录。
        /// </summary>
        [ForeignKey("ProductCatalog")]
        public Guid CatalogId { get; set; }
    }
}
