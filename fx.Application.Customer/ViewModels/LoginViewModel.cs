﻿using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Application.Customer.ViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 登录用户Id
        /// </summary>
        public string LoginId { get; set; }
        /// <summary>
        /// 登录密码。
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 返回的URL地址
        /// </summary>
        public string ReturnUrl { get; set; }

    }
}
