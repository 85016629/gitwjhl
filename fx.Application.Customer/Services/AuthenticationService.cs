using fx.Domain.core;
using fx.Domain.Customer;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using fx.Domain.Bus;

namespace fx.Application.Customer
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMemoryBus _bus;
        public AuthenticationService(ICustomerRepository repository, IMemoryBus bus)
        {
            _repository = repository;
            _bus = bus;
            //_bus.RegisterEventHandler<LoginSuccessed, CustomerEventHandler>();
            //_bus.RegisterCommandHandler<UpdateLastLoginTimeCommand, CustomerCommandExecutor>();
        }


        public void LogOut(string userLoginId)
        {
            throw new System.NotImplementedException();
        }

        public bool Login(string userLoginId, string password)
        {
            bool result = true;
            try
            {                
                var user = _repository.QueryCustomerByIdAndPwd(userLoginId, password);
                if (user == null)
                {
                    throw new Exception("该用户还为注册");
                }

                var loginSuccessed = new LoginSuccessed
                {
                    LoginId = userLoginId,
                    EventId = Guid.NewGuid(),
                    EventData = JsonConvert.SerializeObject(user),
                    AggregateRootType = user.GetType().Name
                };
                _bus.RaiseEvent(loginSuccessed);
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}