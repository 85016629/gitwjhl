using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    /// <summary>
    /// 消息订阅者
    /// </summary>
    public interface IMessageSubscriber : IMessageQueue
    {
        object Subscribe();
    }
}
