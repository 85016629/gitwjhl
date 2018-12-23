using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IRepository<T,Tkey> where T : IAggregateRoot
    {
        void Add(T entity);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<T> FindByIdAsync(Tkey id);
        Task<T> FindByIds(object[] ids);
        IQueryable<T> GetAll();
    }
}
