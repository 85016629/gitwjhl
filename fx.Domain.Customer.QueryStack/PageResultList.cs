using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.CustomerContext.QueryStack
{
    public class PageResultList<T> where T : class
    {
        public int TotalRecords { get; set; }

        public List<T> ResultList { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
