using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.IdentityService.ViewModels
{
    public class LoginViewModel
    {
        public string UserLoginId { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
