using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fx.Domain.ProductContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fx.Product.WebApi.Controllers
{
    /// <summary>
    /// 产品目录。
    /// </summary>
    [Route("api/Product-Management/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }
        /// <summary>
        /// 创建一个新的产品目录。
        /// </summary>
        /// <param name="catalogName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Catalogs")]
        public IActionResult CreateNewCatalog(string catalogName)
        {
            _catalogService.AddCatalog(catalogName);
            return Ok();
        }
    }
}