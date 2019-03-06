using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fx.Domain.core.FunctionMenu
{
    public class MenuItem : AggregateRoot<int>
    {
        [Key]
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public MenuItem ParentMenu { get; set; }
        public IEnumerable<MenuItem> ChildrenMenus { get; set; }
    }
}
