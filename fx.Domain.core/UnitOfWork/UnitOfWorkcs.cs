using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public class UnitOfWorkcs : IUnitOfWork
    {
        protected DbContext context;
        public UnitOfWorkcs(DbContext db)
        {
            context = db;
        }
        public async Task<bool> CommitAaync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
