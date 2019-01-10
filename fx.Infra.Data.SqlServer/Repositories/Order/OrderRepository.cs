using fx.Domain.OrderContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.Data.SqlServer
{
    public class OrderRepository : BaseRepository<Order, string>, IOrderRepository
    {
        
    }
}