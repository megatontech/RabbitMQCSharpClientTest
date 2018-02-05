namespace TJAP.Common.Tools.MessageQueue.Config
{
    /// <summary>
    /// 消息队列相关配置的DOM。
    /// </summary>
    public class MqConfigDom
    {
        /// <summary>
        /// 消息队列的发送地址。
        /// </summary>
        public string MqSendHost { get; set; }

        /// <summary>
        /// 消息队列的监听地址。
        /// </summary>
        public string MqListenHost { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string MqUserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string MqPassword { get; set; }

        /// <summary>
        /// 客户端默认发送的队列名称
        /// </summary>
        public string MqSendQueueName { get; set; }

        /// <summary>
        /// 客户端默认监听的队列名称
        /// </summary>
        public string MqListenQueueName { get; set; }
    }
}