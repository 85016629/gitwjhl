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
        int Update(T entity);
        Task<int> UpdateAsync(T entity);
        Task<T> FindByIdAsync(Tkey id);
        Task<T> FindByIdsAsync(object[] ids);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        int Delete(Tkey id);
        Task<int> DeleteAsync(Tkey id);
    }
}
