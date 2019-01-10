using fx.Domain.core;
using System;

namespace fx.Domain.CustomerContext
{
    public class RegisterCustomerCommand : BaseCommand
    {
        private string _name;
        private DateTime _registerTime;
        private string _mobile;
        private string _loginId;

        public RegisterCustomerCommand(string name) {
            _name = name;
        }

        public RegisterCustomerCommand() { }

        public string Name { get => _name; set => _name = value; }
        public DateTime RegisterTime { get => _registerTime; set => _registerTime = value; }
        public string Mobile { get => _mobile; set => _mobile = value; }
        public string LoginId { get => _loginId; set => _loginId = value; }
    }
}
