using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fx.Application.Customer.ViewModels
{
    /// <summary>
    /// 用户注册视图模型。
    /// </summary>
    public class RegisterViewModel
    {
        [Display(Description ="用户名")]
        public string Username { get; set; }
        [Required(ErrorMessage ="用户名不能为空")]
        [MinLength(6)]
        [MaxLength(20)]        
        public string LoginId { get; set; }
        [Required(ErrorMessage ="密码不能为空")]
        [MinLength(8)]
        [MaxLength(20)]
        [Display(Description ="用户密码")]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

    }
}
