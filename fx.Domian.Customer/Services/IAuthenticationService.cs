using System.Threading.Tasks;

namespace fx.Domain.Customer
{
    public interface IAuthenticationService
    {
        bool Login(string userLoginId, string password);

        void LogOut(string userLoginId);
    }
}