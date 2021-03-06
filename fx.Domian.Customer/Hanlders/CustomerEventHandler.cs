﻿using System.Threading;
using System.Threading.Tasks;
using fx.Domain.Bus;
using fx.Domain.core;
using MediatR;

namespace fx.Domain.CustomerContext
{
    public class CustomerEventHandler :
        INotificationHandler<LoginSuccessed>
    {
        private IMemoryBus _bus;
        public CustomerEventHandler(IMemoryBus bus)
        {
            _bus = bus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(LoginSuccessed @event, CancellationToken cancellationToken)
        {
            var updateLoastLogintimeCommand = new UpdateLastLoginTimeCommand(@event.LoginId)
            {
                CommandId = new System.Guid(),
            };

            await _bus.SendCommand(updateLoastLogintimeCommand);
        }
    }
}