using fx.Domain.core;
using System;
using System.Threading.Tasks;

namespace fx.Domain.Customer
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMemoryBus _bus;
        public AuthenticationService(ICustomerRepository repository, IMemoryBus bus)
        {
            _repository = repository;
            _bus = bus;
        }


        public void LogOut(string userLoginId)
        {
            throw new System.NotImplementedException();
        }

        public bool Login(string userLoginId, string password)
        {
            var user = _repository.QueryCustomerByIdAndPwd(userLoginId, password);
            if (user == null)
                throw new Exception("改用户还为注册");

            var loginSuccessed = new LoginSuccessed
            {
                CustomerId = userLoginId,
                EventId = Guid.NewGuid()
            };

            _bus.RaiseEvent(loginSuccessed);

            return true;
        }
    }
}