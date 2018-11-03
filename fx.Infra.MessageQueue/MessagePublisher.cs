using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Infra.MessageQueue
{
    public class MessagePublisher
    {
        private const string ExchangeName = "fx.exchange";
        const string QueueName = "fx.queue";

        const string TopExchangeName = "topic.fx.exchange";

        //队列名称
        const string TopQueueName = "topic.fx.queue";

        private static readonly ConnectionFactory rabbitMqFactory = new ConnectionFactory
        {
            HostName = "192.168.3.119",
            UserName = "guest",
            Password = "guest",
            Port = 5672
        };

        public static void DirectExchangeSendMsg()
        {
            using (IConnection conn = rabbitMqFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);
                    channel.QueueDeclare(QueueName, durable: true, autoDelete: false, exclusive: false, arguments: null);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);//告诉broker同一时间只处理一个消息
                                                                                       //channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var msgBody = Encoding.UTF8.GetString(ea.Body);
                        Console.WriteLine(string.Format("***接收时间:{0}，消息内容：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msgBody));
                        int dots = msgBody.Split('.').Length - 1;
                        System.Threading.Thread.Sleep(dots * 1000);
                        Console.WriteLine(" [x] Done");
                        //处理完成，告诉Broker可以服务端可以删除消息，分配新的消息过来
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };
                    //noAck设置false,告诉broker，发送消息之后，消息暂时不要删除，等消费者处理完成再说
                    channel.BasicConsume(QueueName, autoAck: false, consumer: consumer);

                    Console.WriteLine("按任意值，退出程序");
                    Console.ReadKey();
                }
            }
        }

        public static void TopicExchangeSendMsg()
        {
            using (IConnection conn = rabbitMqFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.ExchangeDeclare(TopExchangeName, "topic", durable: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(TopQueueName, durable: false, autoDelete: false, exclusive: false, arguments: null);
                    channel.QueueBind(TopQueueName, TopExchangeName, routingKey: TopQueueName);
                    //var props = channel.CreateBasicProperties();
                    //props.Persistent = true;
                    string vadata = Console.ReadLine();
                    while (vadata != "exit")
                    {
                        var msgBody = Encoding.UTF8.GetBytes(vadata);
                        channel.BasicPublish(exchange: TopExchangeName, routingKey: TopQueueName, basicProperties: null, body: msgBody);
                        Console.WriteLine(string.Format("***发送时间:{0}，发送完成，输入exit退出消息发送", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        vadata = Console.ReadLine();
                    }
                }
            }
        }
    }
}
