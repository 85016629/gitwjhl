using System;

namespace fx.Test.WinConsole
{
    using fx.Infra.MessageQueue;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            var msgProducer = new MessagePublisher();
            

            msgProducer.DirectExchangeSendMsg("hello world!");

            Thread.Sleep(500);



        }
    }
}
