using fx.Domain.core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fx.Infra.Data.SqlServer.User
{
    public class UserRepository : BaseRepository<BaseUser, Guid>, IUserRepository
    {
        private SqlDbContext DbContext = new SqlDbContext();
        public UserRepository(DbContextOptions options)
        {

        }
        public BaseUser GetUserByLoginIdAndPassword(string loginId, string password)
        {
            var user = DbContext.BaseUsers.Where(u => u.LoginId == loginId && u.Password == password).FirstOrDefault();
            return user;
        }

        public BaseUser SearchUsersByUUId(Guid id)
        {
            var user = DbContext.BaseUsers.Where(u => u.UUId == id).FirstOrDefault();
            var relations = DbContext.UserRoleRelations.Include(r => r.Role)
                    .Where(r => r.UserId == user.UUId)
                    .ToList();
            user.UserRoles = relations;

            //如果要序列化Json对象，参考下面的代码。
            //JsonConvert.SerializeObject(user, Formatting.None, new JsonSerializerSettings()
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //})

            return user;
        }
    }
}
