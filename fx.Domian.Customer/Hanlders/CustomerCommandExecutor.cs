using fx.Domain.core;
using fx.Infra.MemoryCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using AutoMapper;
using fx.Domain.CustomerContext.Commands;

namespace fx.Domain.CustomerContext
{
    public class CustomerCommandExecutor :
        IRequestHandler<UpdateLastLoginTimeCommand, object>,
        IRequestHandler<RegisterCustomerCommand, object>,
        IRequestHandler<LoginCommand, object>,
        IRequestHandler<ResetPasswordCommand, object>
    {
        protected ICustomerRepository _storage;
        protected IMemoryBus _bus;

        public CustomerCommandExecutor(IMemoryBus bus, ICustomerRepository repository)
        {
            _storage = repository ?? throw new ArgumentNullException(nameof(repository));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task<object> Handle(UpdateLastLoginTimeCommand request, CancellationToken cancellationToken)
        {
            //var user = null; //_storage.FindByLoginId(request.UserLoginId);

            //if (await _storage.UpdateAsync(user) > 0)
            //{
            //    //_memoryCache.WriteInCache(user.LoginId, JsonConvert.SerializeObject(user));
            //    return Task.FromResult((object)"执行成功");+
            //}

            return await Task.FromResult((object)"执行失败");
        }

        public async Task<object> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer()
            {
                MobilePhone = request.Mobile,
                RegisterTime = DateTime.Now,
                Username = request.Name,
                LoginId = request.LoginId
            };
            return Task.FromResult((object)await _storage.AddAsync(customer));
        }

        public Task<object> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var customer = _storage.Login(request.LoginId, request.Password);
            return Task.FromResult((object)customer);
        }

        public Task<object> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var customer = _storage.Login(request.LoginId, request.OldPasswd);
            if (customer == null)
                throw new AppBusinessException("CST.ERR.001", "当前客户密码错误！");

            return Task.FromResult((object)_storage.ResetPassword(request.LoginId, request.NewPasswd));
        }
    }
}
