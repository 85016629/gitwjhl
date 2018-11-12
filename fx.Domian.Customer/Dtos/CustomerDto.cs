using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fx.Domain.Customer
{
    public class CustomerDto
    {
        private int _state;
        private string _name;

        public string Name { get => _name; set => _name = value; }
        public DateTime RegisterTime { get; set; }
        public string Mobile1 { get; set; }
        public string QQ { get; set; }
        public int State { get => _state; set => _state = value; }
        [Key]
        public string LoginId { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string Passwd { get; set; }

    }
}
