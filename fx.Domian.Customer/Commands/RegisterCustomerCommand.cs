using fx.Domain.core;
using System;

namespace fx.Domain.Customer
{
    public class RegisterCustomerCommand : ICommand
    {
        private int _state;
        private string _name;
        private DateTime _registerTime;
        private string _mobile1;
        private string _mobile2;
        private string _mobile3;
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
        public string Mobile1 { get => _mobile1; set => _mobile1 = value; }
        public string Mobile2 { get => _mobile2; set => _mobile2 = value; }
        public string Mobile3 { get => _mobile3; set => _mobile3 = value; }
        public string QQ { get => _qQ; set => _qQ = value; }
        public int State { get => _state; set => _state = value; }
        public string LoginId { get => _loginId; set => _loginId = value; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get => _iDNumber; set => _iDNumber = value; }
        public string Remarks { get => _remarks; set => _remarks = value; }
        Guid ICommand.CommandId        {            get; set;        }
    }
}
