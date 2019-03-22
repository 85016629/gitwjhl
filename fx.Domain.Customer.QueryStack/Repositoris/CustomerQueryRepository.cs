using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using fx.Domain.CustomerContext.QueryStack.Models;
using Microsoft.EntityFrameworkCore;

namespace fx.Domain.CustomerContext.QueryStack.Repositoris
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private static readonly CustomerQueryDbContext queryDbContext = new CustomerQueryDbContext();
        public IQueryable<CustomerDto> GetAllCustomers(int pageIndex, int pageSize, out int totalRecords)
        {
            var customers = queryDbContext.CustomerDtos
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            totalRecords = queryDbContext.CustomerDtos.Count();

            return customers;
        }

        public IQueryable<CustomerDto> SearchCustomers(string username, int pageIndex, int pageSize, out int totalRecords)
        {
            var customers = queryDbContext.CustomerDtos
                .Where(c => EF.Functions.Like(c.Username, $"%{username}%"))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            totalRecords = queryDbContext.CustomerDtos
                .Where(c => EF.Functions.Like(c.Username, $"%{username}%"))
                .Count();

            return customers;
        }

        public IQueryable<CustomerDto> SearchCustomers(Expression<Func<CustomerDto, bool>> where, Expression<Func<CustomerDto, object>> orderExpression, int pageSize, int pageIndex, out int totalRecords)
        {
            var customers = queryDbContext.CustomerDtos
                .Where(where)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(orderExpression);

            totalRecords = queryDbContext.CustomerDtos
                .Where(where).Count();

            return customers;
        }
    }
}
