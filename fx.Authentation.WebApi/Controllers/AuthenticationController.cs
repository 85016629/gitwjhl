using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using fx.Authentation.WebApi.Models;
using fx.Domain.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fx.Authentation.WebApi.Controllers
{
    /// <summary>
    /// 认证相关控制器
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;

        /// <summary>
        ///  
        /// </summary>
        /// <param name="authenticationService"></param>
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// 用户登录。
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [Route("/login")]
        [HttpPut]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!_authenticationService.Login(loginViewModel.UserLoginId, loginViewModel.UserPwd))
                return Content("登录失败");

            return Ok();
        }
    }
}