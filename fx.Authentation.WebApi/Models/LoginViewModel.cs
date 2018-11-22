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
        [Display(Description ="用户登录ID", Name ="用户唯一Id")]
        [DataType(DataType.Text)]
        [Required]
        [StringLength(maximumLength:30,MinimumLength =4,ErrorMessage ="最小长度4位，最大长度30位")]
        public string UserLoginId { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [Display(Description ="用户密码")]
        [DataType(DataType.Password)]
        [Required]
        public string UserPwd { get; set; }

    }
}
