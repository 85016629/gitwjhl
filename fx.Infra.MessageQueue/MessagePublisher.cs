using fx.Domain.core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.MessageQueue
{
    public class MessagePublisher : IMessagePublisher
    {

        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Passwd { get; set; }
        public int Port { get; set; }

        public void PublishMessage(object msgBody)
        {
            var rabbitMqFactory = new ConnectionFactory
            {
                HostName = Hostname,
                UserName = Username,
                Password = Passwd,
                Port = Port
            };

            using (var connection = rabbitMqFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("myQueue", false, false, false, null);
                    string message = msgBody.ToString();
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", "myQueue", null, body);
                    Console.WriteLine(" set {0}", message);
                }
            }
        }

        #region Old Code
        //private const string ExchangeName = "fx.exchange";
        //const string QueueName = "fx.queue";

        //const string TopExchangeName = "topic.fx.exchange";

        ////队列名称
        //const string TopQueueName = "topic.fx.queue";

        //private readonly ConnectionFactory rabbitMqFactory = new ConnectionFactory
        //{
        //    HostName = "localhost",
        //    UserName = "guest",
        //    Password = "guest",
        //    Port = 5672
        //};

        ///// <summary>
        ///// 直接发送内容
        ///// </summary>
        ///// <param name="content"></param>
        //public void DirectExchangeSendMsg(string content)
        //{
        //    using (var connection = rabbitMqFactory.CreateConnection())
        //    {
        //        using (var channel = connection.CreateModel())
        //        {
        //            channel.QueueDeclare("hello", false, false, false, null);
        //            string message = "Hello World";
        //            var body = Encoding.UTF8.GetBytes(message);
        //            channel.BasicPublish("", "hello", null, body);
        //            Console.WriteLine(" set {0}", message);
        //        }
        //    }
        //}

        //public void TopicExchangeSendMsg()
        //{
        //    using (IConnection conn = rabbitMqFactory.CreateConnection())
        //    {
        //        using (IModel channel = conn.CreateModel())
        //        {
        //            channel.ExchangeDeclare(TopExchangeName, "topic", durable: false, autoDelete: false, arguments: null);
        //            channel.QueueDeclare(TopQueueName, durable: false, autoDelete: false, exclusive: false, arguments: null);
        //            channel.QueueBind(TopQueueName, TopExchangeName, routingKey: TopQueueName);
        //            //var props = channel.CreateBasicProperties();
        //            //props.Persistent = true;
        //            string vadata = Console.ReadLine();
        //            while (vadata != "exit")
        //            {
        //                var msgBody = Encoding.UTF8.GetBytes(vadata);
        //                channel.BasicPublish(exchange: TopExchangeName, routingKey: TopQueueName, basicProperties: null, body: msgBody);
        //                Console.WriteLine(string.Format("***发送时间:{0}，发送完成，输入exit退出消息发送", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        //                vadata = Console.ReadLine();
        //            }
        //        }
        //    }
        //}
        #endregion

    }
}
