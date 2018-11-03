using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public class EventDBContext : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“MembershipContext”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“fx.Infra.Data.Context.MembershipContext”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“MembershipContext”
        //连接字符串。

        public const string SchemaName = "dbo";

        public EventDBContext(string nameOrConnectionString)
            : base()
        {
        }


        public DbSet<DomainEvent> DomainEventStorage { get; set; }
    }
}

