using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using TJAP.Common.Tools.MessageQueue.Common;
using TJAP.Common.Tools.Net;

namespace TJAP.Common.Tools.MessageQueue.Config
{
    /// <summary>
    /// <see cref="TJAP.Common.Tools.MessageQueue.Config.MqConfigDom"/>创建工厂。
    /// </summary>
    internal class MqConfigDomFactory
    {
        /// <summary>
        /// 创建MqConfigDom一个实例。
        /// </summary>
        /// <returns>MqConfigDom</returns>
        internal static MqConfigDom CreateConfigDomInstance(String configChannel)
        {
            return GetConfigFormHttpStting(configChannel);
        }

        /// <summary>
        /// 获取物理配置文件中的配置项目。
        /// <!--begin RabbitMq配置 TEST-->
        /// <add key = "AutoMQClientMqSendHost" value="127.0.0.1" /><!--MQ的地址-->
        /// <add key = "AutoMQClientMqListenHost" value="127.0.0.1" /><!--MQ的地址-->
        /// <add key = "AutoMQClientMqUserName" value="guest" /><!--MQ的用户名-->
        /// <add key = "AutoMQClientMqPassword" value="guest" /><!--MQ的密码-->
        /// <add key = "AutoMQClientMqSendQueueName" value="CALC" /><!--对应于当前项目的队列名称，默认监听的队列-->
        /// <add key = "AutoMQClientMqListenQueueName" value="CALC" /><!--对应于当前项目的队列名称，默认监听的队列-->
        /// <!--end RabbitMq配置-->
        /// </summary>
        /// <returns></returns>
        private static MqConfigDom GetConfigFormAppStting(String configChannel)
        {
            var result = new MqConfigDom();
            var MqSendHost = ConfigurationManager.AppSettings[configChannel + "MqSendHost"];
            if (MqSendHost.IsNullOrEmpty())
                throw new Exception(configChannel + "RabbitMQ发送地址配置错误");
            result.MqSendHost = MqSendHost;
            var MqListenHost = ConfigurationManager.AppSettings[configChannel + "MqListenHost"];
            if (MqListenHost.IsNullOrEmpty())
                throw new Exception(configChannel + "RabbitMQ监听地址配置错误");
            result.MqListenHost = MqListenHost;
            var mqUserName = ConfigurationManager.AppSettings[configChannel + "MqUserName"];
            if (mqUserName.IsNullOrEmpty())
                throw new Exception("RabbitMQ用户名不能为NULL");
            result.MqUserName = mqUserName;
            var mqPassword = ConfigurationManager.AppSettings[configChannel + "MqPassword"];
            if (mqPassword.IsNullOrEmpty())
                throw new Exception("RabbitMQ密码不能为NULL");
            result.MqPassword = mqPassword;
            var MqSendQueueName = ConfigurationManager.AppSettings[configChannel + "MqSendQueueName"];
            if (MqSendQueueName.IsNullOrEmpty())
                throw new Exception("RabbitMQClient 默认发送的MQ队列名称不能为NULL");
            result.MqSendQueueName = MqSendQueueName;
            var mqListenQueueName = ConfigurationManager.AppSettings[configChannel + "MqListenQueueName"];
            if (mqListenQueueName.IsNullOrEmpty())
                throw new Exception("RabbitMQClient 默认侦听的MQ队列名称不能为NULL");
            result.MqListenQueueName = mqListenQueueName;
            return result;
        }

        /// <summary>
        /// 从网络接口获取配置
        /// {
        ///   "MqSendHost": "127.0.0.1",
        ///   "MqListenHost": "127.0.0.1",
        ///   "MqUserName": "guest",
        ///   "MqPassword": "guest",
        ///   "MqSendQueueName": "CALC",
        ///   "MqListenQueueName": "CALC"
        /// }
        /// </summary>
        /// <param name="configChannel"></param>
        /// <returns></returns>
        private static MqConfigDom GetConfigFormHttpStting(String configChannel)
        {
            string url = "http://easy-mock.com/mock/5a0a64eafed5cf26fd6b041a/PROJECT/MqConfigDom/CALC#!method=get";
            string jsonToken = "{\"url\":\"http://WWW.HGSOFT.COM\",\"name\":\"HGSOFT\",\"pwd\":\"HGSOFT\",\"token\":\"e12adf50a03d9d16849c2466291ed1b5\",\"project\":\"HGTEST01\"}";
            string result = WebApiHelper.PostJson(url, jsonToken);
            JObject jinfo = JObject.Parse(result);
            MqConfigDom mqConfigDom = new MqConfigDom();
            var MqSendHost = jinfo["MqSendHost"].ToString();
            if (MqSendHost.IsNullOrEmpty())
                throw new Exception(configChannel + "RabbitMQ发送地址配置错误");
            mqConfigDom.MqSendHost = MqSendHost;
            var MqListenHost = jinfo["MqListenHost"].ToString();
            if (MqListenHost.IsNullOrEmpty())
                throw new Exception(configChannel + "RabbitMQ监听地址配置错误");
            mqConfigDom.MqListenHost = MqListenHost;
            var mqUserName = jinfo["MqUserName"].ToString();
            if (mqUserName.IsNullOrEmpty())
                throw new Exception("RabbitMQ用户名不能为NULL");
            mqConfigDom.MqUserName = mqUserName;
            var mqPassword = jinfo["MqPassword"].ToString();
            if (mqPassword.IsNullOrEmpty())
                throw new Exception("RabbitMQ密码不能为NULL");
            mqConfigDom.MqPassword = mqPassword;
            var MqSendQueueName = jinfo["MqSendQueueName"].ToString();
            if (MqSendQueueName.IsNullOrEmpty())
                throw new Exception("RabbitMQClient 默认发送的MQ队列名称不能为NULL");
            mqConfigDom.MqSendQueueName = MqSendQueueName;
            var mqListenQueueName = jinfo["MqListenQueueName"].ToString();
            if (mqListenQueueName.IsNullOrEmpty())
                throw new Exception("RabbitMQClient 默认侦听的MQ队列名称不能为NULL");
            mqConfigDom.MqListenQueueName = mqListenQueueName;
            return mqConfigDom;
        }
    }
}