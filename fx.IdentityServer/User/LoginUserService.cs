using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.IdentityService
{
    public class LoginUserService : ILoginUserService
    {
        IServiceProvider serviceProvider = null;

        public LoginUserService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider?? throw new NullReferenceException(nameof(serviceProvider));
        }

        public bool Authenticate(string userName, string password, out LoginUser loginUser)
        {
            var userRepository = serviceProvider.GetService(typeof(UserReporitory)) as UserReporitory;

            loginUser =  userRepository.QueryUser(userName, password);

            return loginUser != null;
        }
    }
}
