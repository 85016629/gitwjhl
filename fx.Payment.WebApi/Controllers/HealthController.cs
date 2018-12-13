using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fx.Payment.WebApi.Controllers
{
    /// <summary>
    /// 健康检查Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 健康检查。
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() { return Ok("ok"); }
    }
}