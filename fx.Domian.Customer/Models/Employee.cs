using fx.Domain.core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.CustomerContext.Models
{
    public class Employee : BaseUser
    {
        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary>
        public Address Address { get; set; }

        public EmployeeState State { get; set; }
    }

    public enum EmployeeState
    {
        /// <summary>
        /// 工作中
        /// </summary>
        Working = 0,
        /// <summary>
        /// 已离职
        /// </summary>
        Dimission = 1,
        /// <summary>
        /// 休假中
        /// </summary>
        Vacation = 2
    }

    public class Address
    {
        public Address() {}
        public Address(string address)
        {
            var obj = JsonConvert.DeserializeObject<Address>(address);
            City = obj.City;
            Province = obj.Province;
            County = obj.County;
            Street = obj.Street;
        }
        public string City { get; set; }
        public string Province { get; set; }
        public string County { get; set; }
        public string Street { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public enum Sex : byte
    {
        Male = 0,
        Female = 1,
        Unknown = 9
    }
}
