using fx.Domain.core.FunctionMenu;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        /// <summary>
        /// 创建一个角色，同时创建角色对应的查看权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="roleMenuRelation"></param>
        void CreateRole(Role role, Permission roleMenuRelation);
        /// <summary>
        /// 检查角色是否授权。
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        bool CheckPermission(int roleId, int menuId);
        /// <summary>
        /// 获取所有角色的授权查看菜单。
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IEnumerable<MenuItem> GetAllAuthorizedMenItems(int roleId);
    }
}
