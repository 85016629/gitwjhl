using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        void Add(T entity);
        int Update(T entity);
        Task<int> UpdateAsync(T entity);
        T FindById(int id);
        Task<T> FindByIdAsync(int id);
    }
}
