using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.CustomerContext.Models
{
    public class Department : AggregateRoot<int>
    {        
        public string DepartmentName { get; set; }
        public IList<Department> SubDepartments { get; set; }
    }
}
