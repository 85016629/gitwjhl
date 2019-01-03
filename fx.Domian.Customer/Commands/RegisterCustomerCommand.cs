using fx.Domain.core;
using System;

namespace fx.Domain.CustomerContext
{
    public class RegisterCustomerCommand : BaseCommand
    {
        private int _state;
        private string _name;
        private DateTime _registerTime;
        private string _mobile;
        private string _qQ;
        private string _loginId;
        private string _iDNumber;
        private string _remarks;

        public RegisterCustomerCommand(string name) {
            _name = name;
        }

        public RegisterCustomerCommand() { }

        public string Name { get => _name; set => _name = value; }
        public DateTime RegisterTime { get => _registerTime; set => _registerTime = value; }
        public string Mobile { get => _mobile; set => _mobile = value; }
        public string QQ { get => _qQ; set => _qQ = value; }
        public int State { get => _state; set => _state = value; }
        public string LoginId { get => _loginId; set => _loginId = value; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get => _iDNumber; set => _iDNumber = value; }
        public string Remarks { get => _remarks; set => _remarks = value; }
    }
}
