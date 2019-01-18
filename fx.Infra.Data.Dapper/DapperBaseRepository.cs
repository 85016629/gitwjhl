using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.Data.Dapper
{
    public class DapperBaseRepository<T, TKey> : IRepository<T, TKey> where T : AggregateRoot<TKey>
    {
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIds(object[] ids)
        {
            throw new NotImplementedException();
        }

        public System.Linq.IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
