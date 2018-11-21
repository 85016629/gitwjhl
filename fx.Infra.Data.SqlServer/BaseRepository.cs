using fx.Domain.core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.Data.SqlServer
{
    public class BaseRepository<T, TKey> : IRepository<T, TKey> where T:AggregateRoot<TKey>
    {
        protected readonly SqlDbContext dbContext = new SqlDbContext();
        public void Add(T entity)
        {
            dbContext.Add<T>(entity);
            dbContext.SaveChanges();
        }

        public Task<int> AddAsync(T entity)
        {
            dbContext.Add<T>(entity);
            return dbContext.SaveChangesAsync();
        }

        public T FindById(TKey id)
        {
            return dbContext.Find<T>(id);
        }

        public T FindByIds(object[] ids)
        {
            return dbContext.Find<T>(ids);
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
    }
}
