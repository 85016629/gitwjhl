using fx.Domain.Common.Enums.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace fx.Domain.CustomerContext.QueryStack.Models
{
    [Table("Users")]
    public class CustomerDto
    {
        [Key]
        public Guid UUId { get; set; }
        public string Username { get; set; }        
        public string LoginId { get; set; }
        public DateTime RegisterTime { get; set; }
        public CustomerState State { get; set; }
        public string MobilePhone { get; set; }
        public  VipLevel? VipLevel{ get; set; }
         
    }
}
