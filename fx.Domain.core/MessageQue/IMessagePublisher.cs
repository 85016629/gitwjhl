﻿using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public interface IMessagePublisher : IMessageQueue
    {
        void PublishMessage(object msgBody);
    }
}
