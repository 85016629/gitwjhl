using fx.Domain.core;
using fx.Domain.CustomerContext;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using fx.Domain.Bus;

namespace fx.Application.Customer
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _repository;
        //private readonly IMemoryBus _bus;
        public AuthenticationService(IUserRepository repository)
        {
            _repository = repository;
            //_bus = bus;
        }
        public bool Login(string userLoginId, string password, out BaseUser user)
        {
            bool result = true;
            try
            {
                user = _repository.GetUserByLoginIdAndPassword(userLoginId, password);

                if (user == null)
                {
                    throw new Exception("该用户还为注册");
                }

                var loginSuccessed = new LoginSuccessed
                {
                    LoginId = userLoginId,
                    EventId = Guid.NewGuid(),
                    EventData = JsonConvert.SerializeObject(user, Formatting.None, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }),
                    AggregateRootType = user.GetType().Name
                };
                //_bus.RaiseEvent(loginSuccessed);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(_repository);            
        }

        public void ChangePasword(string userLoginId, string newPassword, string oldPassword)
        {
            var user = _repository.GetUserByLoginIdAndPassword(userLoginId, oldPassword);
            if (user == null)
                throw new Exception("旧密码认证失败");

            _repository.ChangePassword(userLoginId, newPassword);
        }

        public BaseUser GetUserByLoginIdAndPassword(string userLoginId, string password)
        {
            return _repository.GetUserByLoginIdAndPassword(userLoginId, password);
        }

        public void LoginSuccess(string userLoginId)
        {
            throw new NotImplementedException();
        }

        public void LogOut(string userLoginId)
        {
            throw new NotImplementedException();
        }
    }
}