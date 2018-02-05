﻿using System;
using TJAP.Common.Tools.MessageQueue.Client;

namespace TJAP.Common.Tools.MessageQueue
{
    /// <summary>
    /// RabbitMq client 接口。
    /// </summary>
    public interface IRabbitMqClient : IDisposable
    {
        /// <summary>
        /// RabbitMqClient 数据上下文。
        /// </summary>
        RabbitMqClientContext Context { get; set; }

        /// <summary>
        /// 消息被本地激活事件。通过绑定该事件来获取消息队列推送过来的消息。只能绑定一个事件处理程序。
        /// </summary>
        event ActionEvent ActionEventMessage;

        /// <summary>
        /// 触发一个事件，向队列推送一个事件消息。
        /// </summary>
        /// <param name="eventMessage">消息类型实例</param>
        /// <param name="exChange">Exchange名称</param>
        /// <param name="queue">队列名称</param>
        void TriggerEventMessage(EventMessage eventMessage, string exChange, string queue);

        /// <summary>
        /// 开始消息队列的默认监听。
        /// </summary>
        void OnListening();
    }
}