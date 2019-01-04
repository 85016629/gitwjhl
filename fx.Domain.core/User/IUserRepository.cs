using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public interface IUserRepository : IRepository<BaseUser, Guid>
    {
        BaseUser GetUserByLoginIdAndPassword(string loginId, string password);

        BaseUser SearchUsersByUUId(Guid id);
        void ChangePassword(string userLoginId, string newPassword);
    }
}
