using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace fx.Infra.Data.Dapper
{
    /// <summary>
    /// 复杂的分页查询接口。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDapperComplexPageQuery<T> where T : class
    {
        IEnumerable<T> Search(int pageIndex, int pageSize);
        IEnumerable<T> Search(int pageIndex, int pageSize, Expression condition);
    }
}
