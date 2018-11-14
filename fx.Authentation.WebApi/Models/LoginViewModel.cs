using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fx.Authentation.WebApi.Models
{
    /// <summary>
    /// 登录ViewModel
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 用户登录Id
        /// </summary>
        [Display(Description ="用户登录ID")]
        public string UserLoginId { get; set; }
        public string UserPwd { get; set; }

    }
}
