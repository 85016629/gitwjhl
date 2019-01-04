using System;
using System.Threading.Tasks;
using fx.Domain.core;
using fx.Domain.CustomerContext;

namespace fx.Application.Customer
{
    public interface IAuthenticationService : IDisposable
    {
        BaseUser GetUserByLoginIdAndPassword(string userLoginId, string password);
        void LoginSuccess(string userLoginId); 
        void ChangePasword(string userLoginId, string newPassword, string oldPassword);
        void LogOut(string userLoginId);
    }
}