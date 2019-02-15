using fx.Domain.CustomerContext.Models;
using fx.Domain.CustomerContext.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fx.Infra.Data.SqlServer.Repositories.User
{
    public class DepartmentRepository : BaseRepository<Department, int>, IDepartmentRepository
    {
    }
}