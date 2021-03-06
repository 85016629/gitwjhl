﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fx.Application.Customer;
using fx.Application.Customer.ViewModels;
using fx.Domain.CustomerContext.QueryStack;
using fx.Domain.CustomerContext.QueryStack.Models;
using fx.Domain.CustomerContext.QueryStack.Repositoris;
using Microsoft.AspNetCore.Mvc;


namespace fx.Customer.WebApi.Controllers
{
    
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerQueryRepository _customerQueryRepository;
        /// <summary>
        /// 控制器构造函数
        /// </summary>
        public AccountController(ICustomerService customerService, ICustomerQueryRepository customerQueryRepository)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(CustomerService));
            _customerQueryRepository = customerQueryRepository ?? throw new ArgumentNullException(nameof(CustomerQueryRepository));
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterViewModel registerViewModel)
        {
            _customerService.Register(registerViewModel);
            return Ok();
        }

        /// <summary>
        /// 用户登录。
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]        
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var customer = await _customerService.Login(loginViewModel);
            if (customer == null && string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                return Unauthorized();

            return Ok(customer);
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

        /// <summary>
        /// 所有的客户数据。
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("Customers")]
        public IActionResult GetAllCustomer(int pageIndex, int pageSize)
        {
            int totalRecords = 0;
            var customers = _customerQueryRepository.GetAllCustomers(pageIndex, pageSize, out totalRecords);

            var pageList = new PageResultList<CustomerDto>
            {
                TotalRecords = totalRecords,
                ResultList = customers.ToList()
            };

            return Ok(pageList);
        }
    }
}
