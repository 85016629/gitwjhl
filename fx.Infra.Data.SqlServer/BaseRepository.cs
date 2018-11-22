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
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
        }

        public Task<int> AddAsync(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return dbContext.SaveChangesAsync();
        }

        public T FindById(TKey id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public T FindByIds(object[] ids)
        {
            return dbContext.Set<T>().Find(ids);
        }

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public int Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
    }
}
