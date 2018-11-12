using System.Threading.Tasks;
using fx.Domain.Bus;
using fx.Domain.core;

namespace fx.Domain.Customer
{
    public class CustomerEventHandler : IEventHandler<LoginSuccessed>
    {
        private IMemoryBus _bus;
        public CustomerEventHandler(IMemoryBus bus)
        {
            _bus = bus;
        }
        public async Task HandleAsync(LoginSuccessed @event)
        {
            var updateLoastLogintimeCommand = new UpdateLastLoginTimeCommand(@event.LoginId)
            {
                CommandId = new System.Guid(),
            };

           await _bus.SendCommand(updateLoastLogintimeCommand);
        }
    }
}