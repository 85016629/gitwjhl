using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.CustomerContext.Commands
{
    public class LoginCommand : BaseCommand
    {
        public LoginCommand(string loginId, string password)
        {
            LoginId = loginId;
            Password = password;
        }

        public string LoginId { get; set; }
        public string Password { get; set; }

    }
}
