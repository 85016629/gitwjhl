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

        public async Task<int> AddAsync(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return await dbContext.SaveChangesAsync();
        }

        public int Delete(TKey id)
        {
            var entity = dbContext.Set<T>().Find(id);
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChanges();
        }

        public async Task<int> DeleteAsync(TKey id)
        {
            var entity = await dbContext.Set<T>().FindAsync(id);
            dbContext.Set<T>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<T> FindByIdAsync(TKey id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public Task<T> FindByIdsAsync(object[] ids)
        {
            return dbContext.Set<T>().FindAsync(ids);
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync<T>();
        }

        public int Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return await dbContext.SaveChangesAsync();
        }
    }
}
