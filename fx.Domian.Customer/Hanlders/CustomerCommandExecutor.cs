using fx.Domain.core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fx.Domain.Customer
{
    public class CustomerCommandExecutor :
        IRequestHandler<UpdateLastLoginTimeCommand, object>
    {
        protected ICustomerRepository _storage;
        protected IMemoryBus _bus;

        public CustomerCommandExecutor(IMemoryBus bus, ICustomerRepository repository = null)
        {
            _storage = repository ?? throw new ArgumentNullException(nameof(repository));
            _bus = bus;
        }

        public Task<object> Handle(UpdateLastLoginTimeCommand request, CancellationToken cancellationToken)
        {
            var user = _storage.FindById(request.UserLoginId);
            user.UpdateLastLoginTime();
            if (_storage.Update(user) > 0)
            {
                return Task.FromResult((object)"执行成功");
            }

            return Task.FromResult((object)"执行失败");
        }    
    }
}
