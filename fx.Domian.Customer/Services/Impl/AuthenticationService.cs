using System.Threading.Tasks;

namespace fx.Domain.Customer
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICustomerRepository _repository;
        public AuthenticationService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Login(string userLoginId, string password)
        {
            return await _repository.QueryCustomerByIdAndPwd(userLoginId, password);
        }

        public void LogOut(string userLoginId)
        {
            throw new System.NotImplementedException();
        }
    }
}