using fx.Infra.MessageQueue;
using System;

namespace fx.Test.WinconsoleMQServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var msgSubscriber = new MessageSubscriber();
            msgSubscriber.ReceiveMsg();
           
        }
    }
}
