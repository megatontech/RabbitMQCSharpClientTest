using RabbitMQ.Client;
using System;
using TJAP.Common.Tools.MessageQueue.Client;
using TJAP.Common.Tools.MessageQueue.Config;

namespace TJAP.Common.Tools.MessageQueue
{
    /// <summary>
    /// RabbitMQClient创建工厂。
    /// </summary>
    public class RabbitMqClientFactory
    {
        /// <summary>
        /// 创建一个单例的RabbitMqClient实例。
        /// </summary>
        /// <returns>IRabbitMqClient</returns>
        public static IRabbitMqClient CreateRabbitMqClientInstance(Type type)
        {
            IRabbitMqClient MQinst = null;
            switch (type.Name)
            {
                case "AutoMQClient":
                    MQinst = new AutoMQClient
                    {
                        Context = GetContext(type.Name)
                    };
                    break;

                case "CalcMQClient":
                    MQinst = new CalcMQClient
                    {
                        Context = GetContext(type.Name)
                    };
                    break;

                default:
                    break;
            }
            return MQinst;
        }

        /// <summary>
        /// 根据实例名称获取对应配置
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        internal static RabbitMqClientContext GetContext(string typeName)
        {
            MqConfigDom dom = MqConfigDomFactory.CreateConfigDomInstance(typeName);
            return new RabbitMqClientContext
            {
                ListenQueueName = dom.MqListenQueueName,
                SendQueueName = dom.MqSendQueueName,
                InstanceCode = Guid.NewGuid().ToString(),
                ListenConnection = CreateListenConnection(dom),
                SendConnection = CreateSendConnection(dom),
                mqConfigDom = dom
            };
        }

        /// <summary>
        /// 创建一个发送IConnection。
        /// </summary>
        /// <returns></returns>
        internal static IConnection CreateSendConnection(MqConfigDom mqConfigDom)
        {
            const ushort heartbeat = 60;
            var factory = new ConnectionFactory()
            {
                HostName = mqConfigDom.MqSendHost,
                UserName = mqConfigDom.MqUserName,
                Password = mqConfigDom.MqPassword,
                RequestedHeartbeat = heartbeat, //心跳超时时间
                AutomaticRecoveryEnabled = true //自动重连
            };

            return factory.CreateConnection(); //创建连接对象
        }

        /// <summary>
        /// 创建一个接受IConnection。
        /// </summary>
        /// <returns></returns>
        internal static IConnection CreateListenConnection(MqConfigDom mqConfigDom)
        {
            const ushort heartbeat = 60;
            var factory = new ConnectionFactory()
            {
                HostName = mqConfigDom.MqListenHost,
                UserName = mqConfigDom.MqUserName,
                Password = mqConfigDom.MqPassword,
                RequestedHeartbeat = heartbeat, //心跳超时时间
                AutomaticRecoveryEnabled = true //自动重连
            };

            return factory.CreateConnection(); //创建连接对象
        }

        /// <summary>
        /// 创建一个IModel。
        /// </summary>
        /// <param name="connection">IConnection.</param>
        /// <returns></returns>
        internal static IModel CreateModel(IConnection connection)
        {
            return connection.CreateModel(); //创建通道
        }
    }
}