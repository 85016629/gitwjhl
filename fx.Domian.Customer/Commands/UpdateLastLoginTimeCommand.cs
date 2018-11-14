using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.Customer
{
    public class UpdateLastLoginTimeCommand : BaseCommand
    {
        private string _userLoginId;

        public UpdateLastLoginTimeCommand(string userLoginId)
        {
            _userLoginId = userLoginId;
        }
        public string UserLoginId { get => _userLoginId; set => _userLoginId = value; }
        
    }
}
