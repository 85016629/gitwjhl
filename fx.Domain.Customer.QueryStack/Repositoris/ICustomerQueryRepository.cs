using fx.Domain.CustomerContext.QueryStack.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace fx.Domain.CustomerContext.QueryStack.Repositoris
{
    public interface ICustomerQueryRepository
    {
        IQueryable<CustomerDto> GetAllCustomers(int pageIndex, int pageSize, out int totalRecords);

        IQueryable<CustomerDto> SearchCustomers(string username, int pageIndex, int pageSize, out int totalRecords);

        IQueryable<CustomerDto> SearchCustomers(Expression<Func<CustomerDto, bool>> where, 
            Expression<Func<CustomerDto, object>> orderExpression, 
            int pageSize, int pageIndex, out int totalRecords);
    }
}
