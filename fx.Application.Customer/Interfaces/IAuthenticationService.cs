using System.Threading.Tasks;

namespace fx.Application.Customer
{
    public interface IAuthenticationService
    {
        bool Login(string userLoginId, string password);

        void LogOut(string userLoginId);
    }
}