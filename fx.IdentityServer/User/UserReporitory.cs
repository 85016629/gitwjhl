using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.IdentityService
{
    public class UserReporitory
    {
        public IEnumerable<LoginUser> Users = new List<LoginUser>()
        {
            new LoginUser{ Email="123@sina.com", Id = 1, RealName = "张三", Password = "111111", UserName="bob"},
            new LoginUser{ Email="456@sina.com", Id = 2, RealName = "李四", Password = "111111", UserName = "alice"}
        };

        public LoginUser QueryUser(string userName, string password)
        {
            return Users.FirstOrDefault<LoginUser>(c => c.UserName == userName && c.Password == password);
        }
    }
}
