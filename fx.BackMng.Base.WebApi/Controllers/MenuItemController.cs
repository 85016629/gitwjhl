using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fx.Domain.core.FunctionMenu;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fx.BackMng.Base.WebApi.Controllers
{
    []
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        public MenuItemController()
        {

        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] MenuItem menuItem)
        {

        }
    }
}
