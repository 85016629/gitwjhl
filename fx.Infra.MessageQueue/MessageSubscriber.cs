using fx.Domain.core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Infra.MessageQueue
{
    public class MessageSubscriber : IMessageSubscriber
    {
        public string Hostname { get ; set ; }
        public string Username { get ; set ; }
        public string Passwd { get ; set ; }
        public int Port { get ; set ; }

        public object Subscribe()
        {
            object result = null;
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
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("myQueue", true, consumer);
                    Console.WriteLine(" waiting for message.");
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        result = message;
                        Console.WriteLine("Received {0}", message);
                        channel.BasicAck(ea.DeliveryTag, false);
                    };

                    //Console.WriteLine("按下任意键退出");
                    //    while (true)
                    //{
                    //    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                    //    var body = ea.Body;
                    //    var message = Encoding.UTF8.GetString(body);
                    //    Console.WriteLine("Received {0}", message);

                    //}
                }
            }

            return result;
        }

        #region oldcode
        ///// <summary>
        ///// 连接配置
        ///// </summary>
        //private readonly ConnectionFactory rabbitMqFactory = new ConnectionFactory()
        //{
        //    HostName = "localhost",
        //    UserName = "guest",
        //    Password = "guest",
        //    Port = 5672
        //};
        ///// <summary>
        ///// 路由名称
        ///// </summary>
        //const string ExchangeName = "fx.exchange";

        ////队列名称
        //const string QueueName = "fx.queue";


        //public void ReceiveMsg()
        //{
        //    using (var connection = rabbitMqFactory.CreateConnection())
        //    {
        //        using (var channel = connection.CreateModel())
        //        {
        //            channel.QueueDeclare("hello", false, false, false, null);

        //            var consumer = new EventingBasicConsumer(channel);
        //            channel.BasicConsume("hello", true, consumer);

        //            Console.WriteLine(" waiting for message.");

        //            consumer.Received += (model, ea) =>
        //            {
        //                var body = ea.Body;
        //                var message = Encoding.UTF8.GetString(body);
        //                Console.WriteLine("Received {0}", message);
        //                channel.BasicAck(ea.DeliveryTag, false);
        //            };

        //            Console.WriteLine("按下任意键退出");
        //            //    while (true)
        //            //{
        //            //    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

        //            //    var body = ea.Body;
        //            //    var message = Encoding.UTF8.GetString(body);
        //            //    Console.WriteLine("Received {0}", message);

        //            //}
        //        }
        //    }
        //}

        //public void TopicExchangeSendMsg()
        //{
        //    using (IConnection conn = rabbitMqFactory.CreateConnection())
        //    {
        //        using (IModel channel = conn.CreateModel())
        //        {
        //            //channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);
        //            channel.QueueDeclare(QueueName, durable: true, autoDelete: false, exclusive: false, arguments: null);
        //            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);//告诉broker同一时间只处理一个消息
        //                                                                               //channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
        //            var consumer = new EventingBasicConsumer(channel);
        //            consumer.Received += (model, ea) =>
        //            {
        //                var msgBody = Encoding.UTF8.GetString(ea.Body);
        //                Console.WriteLine(string.Format("***接收时间:{0}，消息内容：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msgBody));
        //                int dots = msgBody.Split('.').Length - 1;
        //                System.Threading.Thread.Sleep(dots * 1000);
        //                Console.WriteLine(" [x] Done");
        //                //处理完成，告诉Broker可以服务端可以删除消息，分配新的消息过来
        //                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        //            };
        //            //noAck设置false,告诉broker，发送消息之后，消息暂时不要删除，等消费者处理完成再说
        //            channel.BasicConsume(QueueName, autoAck: false, consumer: consumer);

        //            Console.WriteLine("按任意值，退出程序");
        //            Console.ReadKey();
        //        }
        //    }
        //}


        //public void DirectAcceptExchangeEvent()
        //{
        //    using (IConnection conn = rabbitMqFactory.CreateConnection())
        //    {
        //        using (IModel channel = conn.CreateModel())
        //        {
        //            //channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);
        //            channel.QueueDeclare(QueueName, durable: true, autoDelete: false, exclusive: false, arguments: null);
        //            //channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
        //            var consumer = new EventingBasicConsumer(channel);
        //            consumer.Received += (model, ea) =>
        //            {
        //                var msgBody = Encoding.UTF8.GetString(ea.Body);
        //                Console.WriteLine(string.Format("***接收时间:{0}，消息内容：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msgBody));
        //            };
        //            channel.BasicConsume(QueueName, autoAck: true, consumer: consumer);

        //            //已过时用EventingBasicConsumer代替
        //            //var consumer2 = new QueueingBasicConsumer(channel);
        //            //channel.BasicConsume(QueueName, noAck: true, consumer: consumer);
        //            //var msgResponse = consumer2.Queue.Dequeue(); //blocking
        //            //var msgBody2 = Encoding.UTF8.GetString(msgResponse.Body);

        //            Console.WriteLine("按任意值，退出程序");
        //            Console.ReadKey();
        //        }
        //    }
        //}
        #endregion

    }
}
