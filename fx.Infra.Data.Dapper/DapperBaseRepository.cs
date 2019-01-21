using Dapper.Contrib.Extensions;
using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.Data.Dapper
{
    public class DapperBaseRepository<T, TKey> : IRepository<T, TKey> where T : AggregateRoot<TKey>
    {
        public async void Add(T entity)
        {
            await DbContext.InsertAsync<T, TKey>(entity);
        }

        public async Task<int> AddAsync(T entity)
        {
            return await DbContext.InsertAsync<T,TKey>(entity);
        }

        public int Delete(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindByIdAsync(TKey id)
        {
            return await DbContext.GetAsync<T,TKey>(id);
        }

        public Task<T> FindByIdsAsync(object[] ids)
        {
            throw new NotImplementedException();
        }

        public  IEnumerable<T> GetAll()
        {
            return DbContext.GetAll<T, TKey>();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            return await DbContext.UpdateAsync<T, TKey>(entity);
        }
    }
}
