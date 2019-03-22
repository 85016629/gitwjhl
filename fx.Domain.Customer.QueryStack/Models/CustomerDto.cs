using fx.Domain.Common.Enums.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.CustomerContext.QueryStack.Models
{
    public class CustomerDto
    {
        public string Username { get; set; }
        public string LoginId { get; set; }
        public DateTime RegisterTime { get; set; }
        public CustomerState State { get; set; }
        public string MobilePhone { get; set; }
        public  VipLevel VipLevel{ get; set; }
         
    }
}
