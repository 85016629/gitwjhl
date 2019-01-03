using System;
using System.Threading.Tasks;
using fx.Domain.core;
using fx.Domain.CustomerContext;

namespace fx.Application.Customer
{
    public interface IAuthenticationService : IDisposable
    {
        bool Login(string userLoginId, string password, out BaseUser user);
        void LogOut(string userLoginId);
    }
}