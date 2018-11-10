using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IRepository<T, TKey> where T : AggregateRoot<TKey>
    {
        Task Add(T entity);
        int Update(T entity);
        Task<TKey> UpdateAsync(T entity);
        T FindById(TKey id);
        Task<T> FindByIdAsync(TKey id);
        Task Delete(TKey id);
    }
}
