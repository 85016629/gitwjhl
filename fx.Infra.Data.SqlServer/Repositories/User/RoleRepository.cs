using fx.Domain.core;
using fx.Domain.core.FunctionMenu;
using fx.Infra.Data.SqlServer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fx.Infra.Data.SqlServer
{
    public class RoleRepository : BaseRepository<Role, int>, IRoleRepository
    {
        public bool CheckPermission(int roleId, int menuId)
        {
            var permission = dbContext.Set<Permission>().Where(p => p.RoleId == roleId).SingleOrDefault();
            var ids = permission.MenuItemIds.Split(',');
            return ids.Contains(menuId.ToString());
        }

        public void CreateRole(Role role, Permission permission)
        {
            dbContext.Set<Role>().Add(role);
            dbContext.Set<Permission>().Add(permission);
            dbContext.SaveChanges();
        }

        public IEnumerable<MenuItem> GetAllAuthorizedMenItems(int roleId)
        {
            return dbContext.Database.SqlQuery<MenuItem>("Select * From MenuItems Where MenuItemId in (Select MenuItemIds From Permissions)");
        }
    }
}
