using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IRepository
    {
        void Add<T>(T entity) where T: IAggregateRoot;
        Task<int> AddAsync<T>(T entity);
        int Update<T>(T entity);
    }
}
