using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using fx.Authentication.WebApi.Models;
using fx.Domain.Customer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fx.Authentication.WebApi.Controllers
{
    /// <summary>
    /// 认证相关控制器
    /// </summary>    
    [Route("AuthenticationService/[controller]")]
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
        /// <param name="userLoginId"></param>
        /// <param name="password"></param>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            //if (!_authenticationService.Login(loginViewModel.UserLoginId, loginViewModel.UserPwd))
            //    return Content("登录失败"); 

            return Ok("登录成功.");
        }

        /// <summary>
        /// 用户登录。
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous]
        [Route("info")]
        public IActionResult Get(string cc)
        {
            //if (!_authenticationService.Login(loginViewModel.UserLoginId, loginViewModel.UserPwd))
            //    return Content("登录失败"); 

            return Ok("登录成功.");
        }

        /// <summary>
        /// 用户登录。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous]
        [Route("info/{id}")]
        public IActionResult Delete(string id)
        {
            //if (!_authenticationService.Login(loginViewModel.UserLoginId, loginViewModel.UserPwd))
            //    return Content("登录失败"); 

            return Ok("删除成功.");
        }

        /// <summary>
        /// 退出登录。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout(string logoutId)
        {

            return await Task.FromResult(Ok(""));
        }
    }
}