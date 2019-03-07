using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fx.Application.Customer.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace fx.Customer.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        public AccountController()
        {

        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterViewModel registerViewModel)
        {
            return Ok();
        }

        /// <summary>
        /// 用户登录。
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]        
        public IActionResult Login()
        {
            return Ok();
        }
        /// <summary>
        /// 用户退出登录。
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
        /// <summary>
        /// 充值密码
        /// </summary>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword()
        {
            return Ok();
        }
    }
}
