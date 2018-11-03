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
        private int _id;
        public string Name { get => _name; set => _name = value; }
        public int Id { get => _id; set => _id = value; }
        public DateTime RegisterTime { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
        public string QQ { get; set; }
        public int State { get => _state; set => _state = value; }
        public string LoginId { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [Key]
        public string IDNumber { get; set; }
        public string Remarks { get; set; }
    }
}
