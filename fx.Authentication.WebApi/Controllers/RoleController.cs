using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fx.Domain.core;

namespace fx.Authentication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleRepository _roleRepository = null;
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new NullReferenceException(nameof(roleRepository));
        }

        /// <summary>
        /// 创建一个新角色.
        /// </summary>
        /// <returns></returns>
        [Route("[controller]/Roles")]
        [HttpPost]
        public async Task<IActionResult> CreateNewRole(string roleName)
        {
            if (await _roleRepository.AddAsync(new Role(roleName)) > 0)
            {

            }
            return Ok();
        }
    }
}